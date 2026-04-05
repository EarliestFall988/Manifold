namespace Api.Web.TypeGen;

[AttributeUsage(AttributeTargets.Property)]
public sealed class TsIgnoreAttribute : Attribute;

[AttributeUsage(AttributeTargets.Property)]
public sealed class TsNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
