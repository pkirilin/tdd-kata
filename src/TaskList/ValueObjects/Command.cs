namespace TaskList.ValueObjects;

public class Command
{
    public string Type { get; }
    public string? ArgumentsText { get; }
    
    public Command(string commandText)
    {
        var commandTextParts = commandText.Split(new[] { ' ' }, 2);
        Type = commandTextParts[0];
        ArgumentsText = commandTextParts.ElementAtOrDefault(1);
    }
}