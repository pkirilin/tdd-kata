using TaskList.Features;
using TaskList.Features.DeleteTask;
using TaskList.ValueObjects;

namespace TaskList.Actions;

public class DeleteAction : IAction
{
    private readonly IHandler<DeleteTaskCommand> _deleteTaskHandler;

    public DeleteAction(IHandler<DeleteTaskCommand> deleteTaskHandler)
    {
        _deleteTaskHandler = deleteTaskHandler;
    }
    
    public string CommandType => "delete";
    
    public void Execute(string? argumentsInputText)
    {
        if (string.IsNullOrWhiteSpace(argumentsInputText))
        {
            throw new ArgumentNullException(nameof(argumentsInputText));
        }
        
        var taskId = new TaskId(argumentsInputText);
        var command = new DeleteTaskCommand(taskId);
        _deleteTaskHandler.Handle(command);
    }
}