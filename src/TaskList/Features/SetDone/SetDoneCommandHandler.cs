using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Features.SetDone;

public class SetDoneCommandHandler : IHandler<SetDoneCommand>
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public SetDoneCommandHandler(IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
    }
    
    public void Handle(SetDoneCommand command)
    {
        var taskId = new TaskId(command.TaskId);
        var taskToUpdate = _projectsService.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", taskId);
            return;
        }

        taskToUpdate.Done = command.Done;
    }
}