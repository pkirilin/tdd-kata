using TaskList.Services;

namespace TaskList.Commands.Add;

public class AddCommandFactory : ICommandFactory
{
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;
    private readonly string _entityName;
    private readonly string? _args;
    
    public AddCommandFactory(string? commandLineArgs, IClock clock, IProjectsService projectsService)
    {
        _clock = clock;
        _projectsService = projectsService;

        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }
        
        var tokens = commandLineArgs.Split(new[] { ' ' }, 2);
        _entityName = tokens[0];

        if (tokens.Length > 1)
        {
            _args = tokens[1];
        }
    }
    
    public ICommand CreateCommand()
    {
        return _entityName switch
        {
            "project" => new AddProjectCommand(_args, _clock, _projectsService),
            "task" => new AddTaskCommand(_args, _projectsService),
            _ => throw new NotImplementedException()
        };
    }
}