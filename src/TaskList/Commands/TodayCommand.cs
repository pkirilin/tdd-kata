using TaskList.Services;

namespace TaskList.Commands;

public class TodayCommand : CommandBase
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public TodayCommand(IProjectsService projectsService, IConsole console) : base(string.Empty)
    {
        _projectsService = projectsService;
        _console = console;
    }

    public override void Execute()
    {
        var projects = _projectsService.GetAll();
        
        foreach (var project in projects)
        {
            var tasksDueToday = project.GetTasksDueToday();

            if (!tasksDueToday.Any())
            {
                continue;
            }
            
            _console.WriteLine(project.Name);
                
            foreach (var task in tasksDueToday)
            {
                _console.WriteLine($"    [{(task.Done ? 'x' : ' ')}] {task.Id}: {task.Description}");
            }
                
            _console.WriteLine();
        }
    }
}