namespace Api.Web.TypeGen;

[AttributeUsage(AttributeTargets.Property)]
public sealed class TsIgnoreAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property)]
public sealed class TsNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

/// <summary>
/// Generates a TypeScript interface for this type but skips OData axios/TanStack Query hook generation.
/// Use this on request/response types for custom (non-OData) endpoints.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class TsQueryGenIgnoreAttribute : Attribute;
