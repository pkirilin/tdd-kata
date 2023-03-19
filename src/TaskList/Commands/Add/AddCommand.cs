namespace TaskList.Commands.Add;

public class AddCommand : ICommand
{
    private readonly ICommandFactory _addCommandFactory;

    public AddCommand(ICommandFactory addCommandFactory)
    {
        _addCommandFactory = addCommandFactory;
    }
    
    public void Execute()
    {
        var command = _addCommandFactory.CreateCommand();
        command.Execute();
    }
}