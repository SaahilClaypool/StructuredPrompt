// See https://aka.ms/new-console-template for more information
using StructuredPrompt;

Console.WriteLine(StructuredPromptGenerator.GenerateStructuredOutputDirections<Outer>());


public class Outer
{
    public int A { get; set; }
    public int? B { get; set; }
    [PromptField("Before Jan 2020")]
    public DateOnly? Date { get; set; }
    [PromptField("Inner class")]
    public Inner? Inner { get; set; }
}

[PromptClass]
public class Inner
{
    public int InnerInt { get; set; }
}
