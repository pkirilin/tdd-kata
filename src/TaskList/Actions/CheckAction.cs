using TaskList.Features;
using TaskList.Features.SetDone;

namespace TaskList.Actions;

public class CheckAction : IAction
{
    private readonly IHandler<SetDoneCommand> _setDoneHandler;

    public CheckAction(IHandler<SetDoneCommand> setDoneHandler)
    {
        _setDoneHandler = setDoneHandler;
    }
    
    public string CommandType => "check";

    public void Execute(string? argumentsInputText)
    {
        _setDoneHandler.Handle(new SetDoneCommand(argumentsInputText, true));
    }
}