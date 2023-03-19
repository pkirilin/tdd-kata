namespace TaskList.ValueObjects;

public class CommandText
{
    public string Type { get; }
    public string[] Arguments { get; }
    public string ArgumentsText => string.Join(' ', Arguments);

    public CommandText(string inputText)
    {
        var inputTextTokens = inputText.Split(new[] { ' ' }, 2);
        Type = inputTextTokens[0];
        Arguments = inputTextTokens.Length > 1 ? new[] { inputTextTokens[1] } : Array.Empty<string>(); 
    }
    
    public CommandText(string inputText, int argumentsCount)
    {
        var inputTextTokens = inputText.Split(new[] { ' ' }, 2);
        Type = inputTextTokens[0];
        Arguments = inputTextTokens.Length > 1
            ? inputTextTokens[1].Split(new[] { ' ' }, argumentsCount)
            : Array.Empty<string>();
    }
}