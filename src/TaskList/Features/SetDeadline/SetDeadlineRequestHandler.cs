using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Features.SetDeadline;

public class SetDeadlineRequestHandler
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public SetDeadlineRequestHandler(IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
    }
    
    public void Handle(SetDeadlineRequest request)
    {
        var taskId = new TaskId(request.TaskId);
        var taskToUpdate = _projectsService.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine($"Could not find a task with an ID of {request.TaskId}");
            return;
        }

        taskToUpdate.SetDeadline(request.Date);
    }
}