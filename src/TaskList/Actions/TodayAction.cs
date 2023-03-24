using TaskList.Features;
using TaskList.Features.ShowTasksDueToday;

namespace TaskList.Actions;

public class TodayAction : IAction
{
    private readonly IHandler<ShowTasksDueTodayQuery> _showTasksDueTodayHandler;

    public TodayAction(IHandler<ShowTasksDueTodayQuery> showTasksDueTodayHandler)
    {
        _showTasksDueTodayHandler = showTasksDueTodayHandler;
    }
    
    public string CommandType => "today";
    
    public void Execute(string? argumentsInputText)
    {
        _showTasksDueTodayHandler.Handle(new ShowTasksDueTodayQuery());
    }
}