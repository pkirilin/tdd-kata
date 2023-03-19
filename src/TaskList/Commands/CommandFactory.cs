using TaskList.Commands.Add;
using TaskList.Commands.Deadline;
using TaskList.Commands.Today;
using TaskList.Services;

namespace TaskList.Commands;

public class CommandFactory : ICommandFactory
{
    private readonly IProjectsService _projectsService;
    private readonly IConsole _console;
    private readonly IClock _clock;
    private readonly string _verb;
    private readonly string? _args;
    
    public CommandFactory(string commandLine, IProjectsService projectsService, IConsole console, IClock clock)
    {
        _projectsService = projectsService;
        _console = console;
        _clock = clock;
        var tokens = commandLine.Split(new[] { ' ' }, 2);
        _verb = tokens[0];

        if (tokens.Length > 1)
        {
            _args = tokens[1];
        }
    }

    public ICommand CreateCommand()
    {
        return _verb switch
        {
            "add" => new AddCommand(new AddCommandFactory(_args, _clock, _projectsService)),
            "deadline" => new DeadlineCommand(_args, _projectsService, _console),
            "today" => new TodayCommand(_projectsService, _console),
            _ => throw new NotImplementedException()
        };
    }
}