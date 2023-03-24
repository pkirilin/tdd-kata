using TaskList.DataAccess;
using TaskList.ValueObjects;

namespace TaskList.Features.SetDeadline;

public class SetDeadlineCommandHandler : IHandler<SetDeadlineCommand>
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IConsole _console;

    public SetDeadlineCommandHandler(IProjectsRepository projectsRepository, IConsole console)
    {
        _projectsRepository = projectsRepository;
        _console = console;
    }
    
    public void Handle(SetDeadlineCommand command)
    {
        var taskId = new TaskId(command.TaskId);
        var taskToUpdate = _projectsRepository.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine($"Could not find a task with an ID of {command.TaskId}");
            return;
        }

        taskToUpdate.SetDeadline(command.Date);
    }
}