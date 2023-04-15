// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Azure.AI.OpenAI;
using StructuredPrompt;

Console.WriteLine();

var prompt = $"""
    Generate one example.
    {StructuredPromptGenerator.GenerateStructuredOutputDirections<Outer>()}
    """;
OpenAIClient client = new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

var options = new ChatCompletionsOptions();
options.Messages.Add(new(ChatRole.User, prompt));
var response = client.GetChatCompletions("gpt-3.5-turbo", options);
var responseMessage = response.Value.Choices[0].Message.Content;
var responseObject = StructuredPromptGenerator.ParseOutput<Outer>(responseMessage);

Console.WriteLine(prompt);
Console.WriteLine(responseMessage);
Console.WriteLine(JsonSerializer.Serialize(responseObject));

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
