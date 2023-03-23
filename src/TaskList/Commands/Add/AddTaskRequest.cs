namespace TaskList.Commands.Add;

public class AddTaskRequest
{
    public string ProjectName { get; }
    public string Id { get; }
    public string Description { get; }
    
    public AddTaskRequest(string? commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }
        
        var parts = commandLineArgs.Split(new[] { ' ' }, 2);
        ProjectName = parts[0];

        var partsWithId = parts[1].Split(new[] { ' ' }, 2);
        Id = partsWithId[0];
        Description = partsWithId[1];
    }
}