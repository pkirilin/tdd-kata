using TaskList.Entities;
using TaskList.Services;

namespace TaskList.Features.AddProject;

public class AddProjectCommandHandler : IHandler<AddProjectCommand>
{
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;

    public AddProjectCommandHandler(IClock clock, IProjectsService projectsService)
    {
        _clock = clock;
        _projectsService = projectsService;
    }
    
    public void Handle(AddProjectCommand command)
    {
        var project = new Project(command.Name, _clock);
        _projectsService.Add(project);
    }
}