using TaskList.Commands.Add;
using TaskList.Commands.Today;
using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Commands;

public class CommandFactory : ICommandFactory
{
    private readonly CommandText _commandText;
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;
    private readonly IClock _clock;

    public CommandFactory(CommandText commandText, IProjectsService projectsService, IConsole console, IClock clock)
    {
        _commandText = commandText;
        _projectsService = projectsService;
        _console = console;
        _clock = clock;
    }

    public ICommand CreateCommand()
    {
        return _commandText.Type switch
        {
            "add" => new AddCommand(
                new AddCommandFactory(
                    new CommandText(_commandText.ArgumentsText, 1),
                    _clock,
                    _projectsService)),
            "today" => new TodayCommand(_projectsService, _console),
            _ => throw new NotImplementedException()
        };
    }
}