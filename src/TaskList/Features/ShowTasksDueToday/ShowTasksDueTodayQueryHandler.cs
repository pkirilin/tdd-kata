using TaskList.DataAccess;

namespace TaskList.Features.ShowTasksDueToday;

public class ShowTasksDueTodayQueryHandler : IHandler<ShowTasksDueTodayQuery>
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IConsole _console;

    public ShowTasksDueTodayQueryHandler(IProjectsRepository projectsRepository, IConsole console)
    {
        _projectsRepository = projectsRepository;
        _console = console;
    }

    public void Handle(ShowTasksDueTodayQuery query)
    {
        var projects = _projectsRepository.GetAll();
        
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