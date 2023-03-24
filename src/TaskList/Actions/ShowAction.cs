using TaskList.Services;

namespace TaskList.Actions;

public class ShowAction : IAction
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public ShowAction(IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
    }
    
    public string CommandType => "show";
    
    public void Execute(string? argumentsInputText)
    {
        var projects = _projectsService.GetAll();
        
        foreach (var project in projects)
        {
            _console.WriteLine(project.Name);
            
            foreach (var task in project.Tasks)
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
            }

            _console.WriteLine();
        }
    }
}