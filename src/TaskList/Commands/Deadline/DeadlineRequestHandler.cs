using TaskList.Services;

namespace TaskList.Commands.Deadline;

public class DeadlineRequestHandler
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public DeadlineRequestHandler(IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
    }
    
    public void Handle(DeadlineRequest request)
    {
        var taskToUpdate = _projectsService.FindTaskById(request.TaskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine($"Could not find a task with an ID of {request.TaskId}");
            return;
        }

        taskToUpdate.SetDeadline(request.Date);
    }
}