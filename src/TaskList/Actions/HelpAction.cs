namespace TaskList.Actions;

public class HelpAction : IAction
{
    private readonly IConsole _console;

    public HelpAction(IConsole console)
    {
        _console = console;
    }
    
    public string CommandType => "help";

    public void Execute(string? argumentsInputText)
    {
        _console.WriteLine("Commands:");
        _console.WriteLine("  show");
        _console.WriteLine("  add project <project name>");
        _console.WriteLine("  add task <project name> <task description>");
        _console.WriteLine("  check <task ID>");
        _console.WriteLine("  uncheck <task ID>");
        _console.WriteLine();
    }
}