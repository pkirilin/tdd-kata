using TaskList.ValueObjects;

namespace TaskList;

public class Application
{
    private const string Quit = "quit";

    private readonly IController _controller;
    private readonly IConsole _console;

    public Application(IController controller, IConsole console)
    {
        _controller = controller;
        _console = console;
    }

    public void Run()
    {
        while (true)
        {
            _console.Write("> ");
            var command = _console.ReadLine() ?? Quit;
            if (command == Quit)
            {
                break;
            }

            Execute(command);
        }
    }

    private void Execute(string commandText)
    {
        var command = new Command(commandText);
        var action = _controller.GetAction(command.Type);

        if (action is null)
        {
            _console.WriteLine("I don't know what the command \"{0}\" is.", command.Type);
            return;
        }
        
        action.Execute(command.ArgumentsText);
    }
}