namespace TaskList.Features.SetDeadline;

public class SetDeadlineCommand
{
    public string TaskId { get; }
    public DateOnly Date { get; }
    
    public SetDeadlineCommand(string? commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }
        
        var args = commandLineArgs.Split(new[] { ' ' }, 2);
        TaskId = args[0];
        Date = DateOnly.Parse(args[1]);
    }
}