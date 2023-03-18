using TaskList.Services;

namespace TaskList.Commands;

public class DeadlineCommand : ICommand
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;

    public DeadlineCommand(
        string commandLineArgs,
        IProjectsService projectsService,
        IConsole console)
    {
        _projectsService = projectsService;
        _console = console;

        var args = commandLineArgs
            .Split(' ')
            .ToArray();
        
        TaskId = ParseTaskId(args);
        DeadlineDate = ParseDeadlineDate(args);
    }
    
    public long TaskId { get; }
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

    private static long ParseTaskId(IReadOnlyList<string> commandLineArgs)
    {
        return long.Parse(commandLineArgs[0]);
    }

    private static DateOnly ParseDeadlineDate(IReadOnlyList<string> commandLineArgs)
    {
        return DateOnly.Parse(commandLineArgs[1]);
    }
}