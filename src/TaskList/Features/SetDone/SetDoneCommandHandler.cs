using TaskList.DataAccess;
using TaskList.ValueObjects;

namespace TaskList.Features.SetDone;

public class SetDoneCommandHandler : IHandler<SetDoneCommand>
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IConsole _console;

    public SetDoneCommandHandler(IProjectsRepository projectsRepository, IConsole console)
    {
        _projectsRepository = projectsRepository;
        _console = console;
    }
    
    public void Handle(SetDoneCommand command)
    {
        var taskId = new TaskId(command.TaskId);
        var taskToUpdate = _projectsRepository.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", taskId);
            return;
        }

        taskToUpdate.Done = command.Done;
    }
}