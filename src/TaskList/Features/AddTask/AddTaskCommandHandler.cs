using TaskList.DataAccess;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Features.AddTask;

public class AddTaskCommandHandler : IHandler<AddTaskCommand>
{
    private readonly IProjectsRepository _projectsRepository;

    public AddTaskCommandHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }
    
    public void Handle(AddTaskCommand command)
    {
        var project = _projectsRepository.FindByName(command.ProjectName);

        if (project is null)
        {
            Console.WriteLine("Could not find a project with the name \"{0}\".", command.ProjectName);
            return;
        }

        var taskId = new TaskId(command.Id);
        var task = new Task(taskId, command.Description);
        project.AddTask(task);
    }
}