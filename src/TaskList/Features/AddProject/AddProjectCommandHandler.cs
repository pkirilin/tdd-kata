using TaskList.DataAccess;
using TaskList.Entities;

namespace TaskList.Features.AddProject;

public class AddProjectCommandHandler : IHandler<AddProjectCommand>
{
    private readonly IClock _clock;
    private readonly IProjectsRepository _projectsRepository;

    public AddProjectCommandHandler(IClock clock, IProjectsRepository projectsRepository)
    {
        _clock = clock;
        _projectsRepository = projectsRepository;
    }
    
    public void Handle(AddProjectCommand command)
    {
        var project = new Project(command.Name, _clock);
        _projectsRepository.Add(project);
    }
}