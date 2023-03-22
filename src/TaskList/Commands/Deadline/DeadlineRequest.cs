using TaskList.ValueObjects;

namespace TaskList.Commands.Deadline;

public class DeadlineRequest
{
    public TaskId TaskId { get; }
    public DateOnly Date { get; }
    
    public DeadlineRequest(string? commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }
        
        var args = commandLineArgs.Split(new[] { ' ' }, 2);
        TaskId = new TaskId(args[0]);
        Date = DateOnly.Parse(args[1]);
    }
}