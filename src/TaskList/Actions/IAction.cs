namespace TaskList.Actions;

public interface IAction
{
    string CommandType { get; }
    
    void Execute(string? argumentsInputText);
}