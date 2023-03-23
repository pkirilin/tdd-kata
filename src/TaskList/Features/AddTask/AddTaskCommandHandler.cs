using TaskList.Services;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Features.AddTask;

public class AddTaskCommandHandler
{
    private readonly IProjectsService _projectsService;

    public AddTaskCommandHandler(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }
    
    public void Handle(AddTaskCommand command)
    {
        var project = _projectsService.FindByName(command.ProjectName);

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