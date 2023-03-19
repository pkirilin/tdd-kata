using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Commands.Deadline;

public class DeadlineCommand : ICommand
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public DeadlineCommand(TaskId taskId, DateOnly deadlineDate, IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;
        TaskId = taskId;
        DeadlineDate = deadlineDate;
    }
    
    public TaskId TaskId { get; }
    public DateOnly DeadlineDate { get; }

    public void Execute()
    {
        var taskToUpdate = _projectsService.FindTaskById(TaskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine($"Could not find a task with an ID of {TaskId}");
            return;
        }

        taskToUpdate.SetDeadline(DeadlineDate);
    }
}