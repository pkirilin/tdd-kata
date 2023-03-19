using TaskList.Entities;
using TaskList.Services;

namespace TaskList.Commands.Add;

public class AddProjectCommand : ICommand
{
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;

    public AddProjectCommand(string? commandLineArgs, IClock clock, IProjectsService projectsService)
    {
        _clock = clock;
        _projectsService = projectsService;
        ProjectName = commandLineArgs ?? throw new ArgumentNullException(nameof(commandLineArgs));
    }
    
    public string ProjectName { get; }
    
    public void Execute()
    {
        var project = new Project(ProjectName, _clock);
        _projectsService.Add(project);
    }
}