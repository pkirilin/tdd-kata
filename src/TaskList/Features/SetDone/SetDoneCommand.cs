namespace TaskList.Features.SetDone;

public class SetDoneCommand
{
    public string TaskId { get; }
    public bool Done { get; }

    public SetDoneCommand(string? commandLineArgs, bool done)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }

        TaskId = commandLineArgs;
        Done = done;
    }
}