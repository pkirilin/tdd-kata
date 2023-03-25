using TaskList.DataAccess;

namespace TaskList.Features.DeleteTask;

public class DeleteTaskCommandHandler : IHandler<DeleteTaskCommand>
{
    private readonly IProjectsRepository _projectsRepository;

    public DeleteTaskCommandHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }
    
    public void Handle(DeleteTaskCommand request)
    {
        var project = _projectsRepository.FindByTaskId(request.Id);
        var task = project?.Tasks.FirstOrDefault(t => t.Id == request.Id);
        
        if (task is null)
        {
            throw new InvalidOperationException($"Task with id = '{request.Id}' not found");
        }
        
        project?.RemoveTask(task);
    }
}