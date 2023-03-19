using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Commands.Deadline;

public class DeadlineCommand : ICommand
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public DeadlineCommand(string? commandLineArgs, IProjectsService projectsService, IConsole console)
    {
        _projectsService = projectsService;
        _console = console;

        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }

        var args = commandLineArgs
            .Split(' ')
            .ToArray();
        
        TaskId = ParseTaskId(args);
        DeadlineDate = ParseDeadlineDate(args);
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

    private static TaskId ParseTaskId(IReadOnlyList<string> commandLineArgs)
    {
        return new TaskId(commandLineArgs[0]);
    }

    private static DateOnly ParseDeadlineDate(IReadOnlyList<string> commandLineArgs)
    {
        return DateOnly.Parse(commandLineArgs[1]);
    }
}