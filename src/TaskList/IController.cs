using TaskList.Actions;

namespace TaskList;

public interface IController
{
    IAction? GetAction(string commandType);
}