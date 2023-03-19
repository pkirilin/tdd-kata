using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Commands.Add;

public class AddCommandFactory : ICommandFactory
{
    private readonly CommandText _commandText;
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;

    public AddCommandFactory(CommandText commandText, IClock clock, IProjectsService projectsService)
    {
        _commandText = commandText;
        _clock = clock;
        _projectsService = projectsService;
    }
    
    public ICommand CreateCommand()
    {
        return _commandText.Type switch
        {
            "project" => new AddProjectCommand(_commandText.Arguments[0], _clock, _projectsService),
            "task" => new AddTaskCommand(
                _commandText.Arguments[0],
                _commandText.Arguments.Length > 1 ? _commandText.Arguments[1] : string.Empty,
                _projectsService),
            _ => throw new NotImplementedException()
        };
    }
}