using TaskList.Features;
using TaskList.Features.SetDone;

namespace TaskList.Actions;

public class UncheckAction : IAction
{
    private readonly IHandler<SetDoneCommand> _setDoneHandler;

    public UncheckAction(IHandler<SetDoneCommand> setDoneHandler)
    {
        _setDoneHandler = setDoneHandler;
    }
    
    public string CommandType => "uncheck";
    
    public void Execute(string? argumentsInputText)
    {
        _setDoneHandler.Handle(new SetDoneCommand(argumentsInputText, true));
    }
}