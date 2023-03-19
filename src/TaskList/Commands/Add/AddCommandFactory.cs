namespace TaskList.Commands.Add;

public class AddCommandFactory : ICommandFactory
{
    private readonly string _entityName;
    private readonly string? _args;
    
    public AddCommandFactory(string commandLineArgs)
    {
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
            "project" => new AddProjectCommand(),
            "task" => new AddTaskCommand(),
            _ => throw new NotImplementedException()
        };
    }
}