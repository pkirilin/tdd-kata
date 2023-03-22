namespace TaskList.Commands.Add;

public class AddProjectRequest
{
    public string Name { get; }
    
    public AddProjectRequest(string? commandLineArgs)
    {
        Name = commandLineArgs ?? string.Empty;
    }
}