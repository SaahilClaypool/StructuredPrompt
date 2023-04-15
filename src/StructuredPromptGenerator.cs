using System;
using System.Linq;
using System.Reflection;

namespace StructuredPrompt;

public static class StructuredPromptGenerator
{
    public static string GenerateStructuredOutputDirections<T>() =>
        GenerateStructuredOutputDirections(typeof(T));

    public static string GenerateStructuredOutputDirections(Type t)
    {
        var schemaFormat = ClassTypeString(t, isRoot: true);
        var prompt = $$"""
        The output should be a markdown code snippet formatted in the following schema:
        ```json
        {{schemaFormat}}
        ```
        """;
        return prompt;
    }

    private static string GetPropertyLine(PropertyInfo prop)
    {
        var attr = prop.GetCustomAttribute<PromptFieldAttribute>();
        var description = string.Empty;
        if (attr != null)
            description = $" // {attr.Description}";
        return $"""
        "{prop.Name}": {JsonTypeName(prop.PropertyType)}{description}
        """;
    }

    private static string JsonTypeName(Type t)
    {
        var optional = string.Empty;
        if (Nullable.GetUnderlyingType(t) is var underlying && underlying != null)
        {
            t = underlying;
            optional = "?";
        }
        var scalars = new[]
        {
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(char),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(nint),
            typeof(nuint),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort),
            typeof(DateTime)
        };

        if (scalars.FirstOrDefault(scalar => scalar.IsAssignableFrom(t) || t.IsAssignableFrom(scalar)) is var scalar && scalar != null)
        {
            return scalar.Name + optional;
        }
        else if (t == typeof(string))
            return "string" + optional;

        return ClassTypeString(t, isRoot: false) + optional;
    }

    private static string ClassTypeString(Type t, bool isRoot)
    {
        var description = string.Empty;
        if (t.GetCustomAttribute<PromptClassAttribute>() is var classAttr && classAttr != null)
        {
            description = classAttr.Description is var d && d != null ?
                $"// {d}" : string.Empty;
        }
        else if (!isRoot)
        {
            return $$"""
            {{t.Name}}
            """;
        }

        return $$"""
            { {{description}}
            {{"\t"}}{{string.Join(",\n\t", t.GetProperties().Select(GetPropertyLine))}}
            }
            """;
    }
}