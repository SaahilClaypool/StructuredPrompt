using System;
namespace StructuredPrompt;
public class PromptFieldAttribute : Attribute
{
    public PromptFieldAttribute(string description)
    {
        Description = description;
    }

    public string Description { get; }
    public bool Ignore { get; }
}