namespace TaskList.Commands.Add;

public class AddTaskRequest
{
    public string ProjectName { get; }
    public string Description { get; }
    
    public AddTaskRequest(string? commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }
        
        var parts = commandLineArgs.Split(new[] { ' ' }, 2);
        ProjectName = parts[0];
        Description = parts[1];
    }
}