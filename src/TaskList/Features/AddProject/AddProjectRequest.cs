namespace TaskList.Features.AddProject;

public class AddProjectRequest
{
    public string Name { get; }
    
    public AddProjectRequest(string? commandLineArgs)
    {
        Name = commandLineArgs ?? string.Empty;
    }
}