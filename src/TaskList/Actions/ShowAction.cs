using TaskList.DataAccess;

namespace TaskList.Actions;

public class ShowAction : IAction
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IConsole _console;

    public ShowAction(IProjectsRepository projectsRepository, IConsole console)
    {
        _projectsRepository = projectsRepository;
        _console = console;
    }
    
    public string CommandType => "show";
    
    public void Execute(string? argumentsInputText)
    {
        var projects = _projectsRepository.GetAll();
        
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