using System;
namespace StructuredPrompt;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class PromptClassAttribute : Attribute
{
    public PromptClassAttribute() { }
    public string? Description { get; set; }
}