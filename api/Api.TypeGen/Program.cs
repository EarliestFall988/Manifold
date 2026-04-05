using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: Api.TypeGen <models-dir> <output-file>");
    return 1;
}

var modelsDir = args[0];
var outputFile = args[1];

if (!Directory.Exists(modelsDir))
{
    Console.Error.WriteLine($"Models directory not found: {modelsDir}");
    return 1;
}

var sourceFiles = Directory.GetFiles(modelsDir, "*.cs", SearchOption.AllDirectories);
var interfaces = new List<string>();
var classNames = new List<string>();        // OData hook generation
var queryIgnoredNames = new List<string>(); // type-only (no hook)

foreach (var file in sourceFiles)
{
    var source = File.ReadAllText(file);
    var tree = CSharpSyntaxTree.ParseText(source);
    var root = tree.GetCompilationUnitRoot();

    var typeDecls = root.DescendantNodes()
        .Where(n => n is ClassDeclarationSyntax or RecordDeclarationSyntax)
        .Cast<MemberDeclarationSyntax>();

    foreach (var typeDecl in typeDecls)
    {
        var (tsInterface, name) = GenerateInterface(typeDecl);
        if (tsInterface is null || name is null) continue;

        interfaces.Add(tsInterface);

        if (HasTypeAttribute(typeDecl, "TsQueryGenIgnore"))
            queryIgnoredNames.Add(name);
        else
            classNames.Add(name);
    }
}

if (interfaces.Count == 0)
{
    Console.WriteLine("No model classes found.");
    return 0;
}

var outputDir = Path.GetDirectoryName(outputFile)!;
Directory.CreateDirectory(outputDir);

var lines = new List<string>
{
    "// AUTO GENERATED with ❤️ by Api.TypeGen",
    "// This file auto-generates all the TypeScript interfaces for the API models from the C# source code (public properties only).",
    "// Last Generated: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + " UTC",
    ""
};
lines.AddRange(interfaces);

File.WriteAllText(outputFile, string.Join("\n", lines) + "\n");
Console.WriteLine($"Generated {interfaces.Count} type(s) → {outputFile}");

if (args.Length >= 3)
{
    var hooksFile = args[2];
    var hooksDir = Path.GetDirectoryName(hooksFile)!;
    Directory.CreateDirectory(hooksDir);

    var hooksLines = new List<string>
    {
        "// AUTO GENERATED with ❤️ by Api.TypeGen",
        "// This file auto-generates React Query hooks for all API models.",
        "// Last Generated: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + " UTC",
        "",
        "import { useQuery, useMutation } from \"@tanstack/react-query\";",
        "import axios from \"axios\";",
        "import type { ODataResponse } from \"@/types/odata\";",
        "import type { " + string.Join(", ", classNames) + " } from \"@/types/api.generated\";",
        ""
    };

    foreach (var name in classNames)
    {
        var getter = $"get{name}";
        var hook = $"use{name}";
        hooksLines.AddRange([
            $"const {getter} = (query?: string) =>",
            $"  axios",
            $"    .get<ODataResponse<{name}>>(`${{import.meta.env.VITE_API_URL}}/odata/{name}${{query ? `?${{query}}` : \"\"}}`)",
            $"    .then((res) => res.data);",
            "",
            $"export const {hook} = (query?: string) =>",
            $"  useQuery({{",
            $"    queryKey: [\"{name}\", query],",
            $"    queryFn: () => {getter}(query),",
            $"  }});",
            "",
            $"export const useCreate{name} = () =>",
            $"  useMutation({{",
            $"    mutationFn: (item: {name}) =>",
            $"      axios",
            $"        .post<{name}>(`${{import.meta.env.VITE_API_URL}}/odata/{name}`, item)",
            $"        .then((res) => res.data),",
            $"  }});",
            "",
            $"export const useUpdate{name} = () =>",
            $"  useMutation({{",
            $"    mutationFn: ({{ key, delta }}: {{ key: number; delta: Partial<{name}> }}) =>",
            $"      axios",
            $"        .patch<{name}>(`${{import.meta.env.VITE_API_URL}}/odata/{name}(${{key}})`, delta)",
            $"        .then((res) => res.data),",
            $"  }});",
            "",
            $"export const useDelete{name} = () =>",
            $"  useMutation({{",
            $"    mutationFn: (key: number) =>",
            $"      axios.delete(`${{import.meta.env.VITE_API_URL}}/odata/{name}(${{key}})`),",
            $"  }});",
            ""
        ]);
    }

    File.WriteAllText(hooksFile, string.Join("\n", hooksLines) + "\n");
    Console.WriteLine($"Generated {classNames.Count} hook(s) → {hooksFile}");
}

if (args.Length >= 4)
{
    var controllersDir = args[3];
    Directory.CreateDirectory(controllersDir);

    var generated = 0;
    var skipped = 0;

    foreach (var file in sourceFiles)
    {
        var source = File.ReadAllText(file);
        var tree = CSharpSyntaxTree.ParseText(source);
        var root = tree.GetCompilationUnitRoot();

        foreach (var classDecl in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
        {
            if (!ImplementsIAudit(classDecl)) continue;

            var name = classDecl.Identifier.Text;
            var keyType = GetKeyType(classDecl);
            var controllerFile = Path.Combine(controllersDir, $"{name}Controller.cs");

            if (File.Exists(controllerFile))
            {
                skipped++;
                continue;
            }

            var plural = $"{name}s";
            var content = string.Join("\n", [
                "using Microsoft.EntityFrameworkCore;",
                "using Api.Web.Database;",
                "using Api.Web.Models;",
                "",
                "namespace Api.Web.Controllers;",
                "",
                $"public class {name}Controller(AppDbContext db) : ManifoldController<{name}, {keyType}>(db)",
                "{",
                $"    protected override DbSet<{name}> Entities => db.{plural};",
                "}",
                ""
            ]);

            File.WriteAllText(controllerFile, content);
            Console.WriteLine($"Generated controller → {controllerFile}");
            generated++;
        }
    }

    if (skipped > 0)
        Console.WriteLine($"Skipped {skipped} existing controller(s).");
}

return 0;

static (string? tsInterface, string? name) GenerateInterface(MemberDeclarationSyntax typeDecl)
{
    var (className, members) = typeDecl switch
    {
        ClassDeclarationSyntax c  => (c.Identifier.Text, c.Members),
        RecordDeclarationSyntax r => (r.Identifier.Text, r.Members),
        _                         => (null, default)
    };

    if (className is null) return (null, null);

    var properties = members
        .OfType<PropertyDeclarationSyntax>()
        .Where(p => p.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)))
        .ToList();

    if (properties.Count == 0) return (null, null);

    var lines = new List<string> { $"export interface {className} {{" };

    foreach (var prop in properties)
    {
        if (HasAttribute(prop, "TsIgnore"))
            continue;

        var isReadonly = IsComputedGetter(prop);
        var tsType = MapType(prop.Type);
        var propName = GetTsName(prop) ?? ToPascalCase(prop.Identifier.Text);
        var readonlyPrefix = isReadonly ? "readonly " : "";
        lines.Add($"  {readonlyPrefix}{propName}: {tsType};");
    }

    lines.Add("}");
    lines.Add("");
    return (string.Join("\n", lines), className);
}

static bool HasTypeAttribute(MemberDeclarationSyntax typeDecl, string name) =>
    typeDecl.AttributeLists
        .SelectMany(al => al.Attributes)
        .Any(a => a.Name.ToString() is var n && (n == name || n == name + "Attribute"));

static bool ImplementsIAudit(ClassDeclarationSyntax classDecl) =>
    classDecl.BaseList?.Types
        .Any(t => t.Type.ToString() == "IAudit") ?? false;

static string GetKeyType(ClassDeclarationSyntax classDecl)
{
    var keyProp = classDecl.Members
        .OfType<PropertyDeclarationSyntax>()
        .FirstOrDefault(p =>
            p.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)) &&
            (p.Identifier.Text == "Id" || HasAttribute(p, "Key")));

    return keyProp is null ? "int" : MapType(keyProp.Type);
}

static bool IsComputedGetter(PropertyDeclarationSyntax prop)
{
    // Expression-bodied: public int Foo => ...
    if (prop.ExpressionBody is not null)
        return true;

    // Accessor list with only a getter (no setter)
    if (prop.AccessorList is not null)
    {
        var accessors = prop.AccessorList.Accessors;
        return accessors.Count == 1 && accessors[0].IsKind(SyntaxKind.GetAccessorDeclaration);
    }

    return false;
}

static string MapType(TypeSyntax typeSyntax)
{
    // T?  (nullable reference or value type)
    if (typeSyntax is NullableTypeSyntax nullable)
        return $"{MapType(nullable.ElementType)} | null";

    // Generic: List<T>, IEnumerable<T>, Dictionary<K,V>, etc.
    if (typeSyntax is GenericNameSyntax generic)
    {
        var outer = generic.Identifier.Text;
        if (outer is "List" or "IList" or "IEnumerable" or "ICollection"
                  or "IReadOnlyList" or "IReadOnlyCollection" or "HashSet" or "ISet")
        {
            var inner = MapType(generic.TypeArgumentList.Arguments[0]);
            return $"{inner}[]";
        }

        if (outer is "Dictionary" or "IDictionary" or "IReadOnlyDictionary")
        {
            var value = MapType(generic.TypeArgumentList.Arguments[1]);
            return $"Record<string, {value}>";
        }
    }

    // T[]
    if (typeSyntax is ArrayTypeSyntax array)
        return $"{MapType(array.ElementType)}[]";

    return typeSyntax.ToString() switch
    {
        "string" or "Guid" or "DateTime" or "DateOnly"
            or "DateTimeOffset" or "TimeOnly" or "TimeSpan" => "string",
        "int" or "long" or "short" or "byte" or "float"
            or "double" or "decimal" or "uint" or "ulong" or "ushort" => "number",
        "bool" => "boolean",
        "object" => "unknown",
        var t => t  // fallback: preserve name (handles references to other model types)
    };
}

static bool HasAttribute(PropertyDeclarationSyntax prop, string name) =>
    prop.AttributeLists
        .SelectMany(al => al.Attributes)
        .Any(a => a.Name.ToString() is var n && (n == name || n == name + "Attribute"));

static string? GetTsName(PropertyDeclarationSyntax prop)
{
    var attr = prop.AttributeLists
        .SelectMany(al => al.Attributes)
        .FirstOrDefault(a => a.Name.ToString() is var n && (n == "TsName" || n == "TsNameAttribute"));

    if (attr?.ArgumentList?.Arguments.FirstOrDefault()?.Expression is LiteralExpressionSyntax lit)
        return lit.Token.ValueText;

    return null;
}

// static string ToCamelCase(string name) =>
//     name.Length == 0 ? name : char.ToLowerInvariant(name[0]) + name[1..];

static string ToPascalCase(string name) =>
    name.Length == 0 ? name : char.ToUpperInvariant(name[0]) + name[1..];
