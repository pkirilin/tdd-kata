namespace TaskList.Features.AddProject;

public class AddProjectCommand
{
    public string Name { get; }
    
    public AddProjectCommand(string? commandLineArgs)
    {
        Name = commandLineArgs ?? string.Empty;
    }
}