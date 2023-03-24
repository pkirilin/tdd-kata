using TaskList.Features;
using TaskList.Features.SetDeadline;

namespace TaskList.Actions;

public class DeadlineAction : IAction
{
    private readonly IHandler<SetDeadlineCommand> _setDeadlineHandler;

    public DeadlineAction(IHandler<SetDeadlineCommand> setDeadlineHandler)
    {
        _setDeadlineHandler = setDeadlineHandler;
    }
    
    public string CommandType => "deadline";
    
    public void Execute(string? argumentsInputText)
    {
        _setDeadlineHandler.Handle(new SetDeadlineCommand(argumentsInputText));
    }
}