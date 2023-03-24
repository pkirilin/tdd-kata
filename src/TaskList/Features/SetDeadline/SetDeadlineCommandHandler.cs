using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Features.SetDeadline;

public class SetDeadlineCommandHandler : IHandler<SetDeadlineCommand>
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public SetDeadlineCommandHandler(IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
    }
    
    public void Handle(SetDeadlineCommand command)
    {
        var taskId = new TaskId(command.TaskId);
        var taskToUpdate = _projectsService.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine($"Could not find a task with an ID of {command.TaskId}");
            return;
        }

        taskToUpdate.SetDeadline(command.Date);
    }
}