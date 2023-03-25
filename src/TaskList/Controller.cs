using Microsoft.Extensions.DependencyInjection;
using TaskList.Actions;

namespace TaskList;

public class Controller : IController
{
    private readonly IReadOnlyDictionary<string, IAction> _actions;

    public Controller(IServiceProvider serviceProvider)
    {
        _actions = serviceProvider
            .GetServices<IAction>()
            .ToDictionary(a => a.CommandType, a => a);
    }
    
    public IAction? GetAction(string commandType)
    {
        return _actions.TryGetValue(commandType, out var action)
            ? action
            : null;
    }
}