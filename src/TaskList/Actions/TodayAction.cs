using TaskList.Features;
using TaskList.Features.GetTasksDueToday;

namespace TaskList.Actions;

public class TodayAction : IAction
{
    private readonly IConsole _console;
    private readonly IHandler<GetTasksDueTodayQuery, GetTasksDueTodayResult> _getTasksDueTodayHandler;

    public TodayAction(
        IConsole console,
        IHandler<GetTasksDueTodayQuery, GetTasksDueTodayResult> getTasksDueTodayHandler)
    {
        _console = console;
        _getTasksDueTodayHandler = getTasksDueTodayHandler;
    }
    
    public string CommandType => "today";
    
    public void Execute(string? argumentsInputText)
    {
        var result = _getTasksDueTodayHandler.Handle(new GetTasksDueTodayQuery());
        
        foreach (var project in result.Projects)
        {
            _console.WriteLine(project.Name);

            foreach (var task in project.Tasks)
            {
                _console.WriteLine($"    [{(task.IsChecked ? 'x' : ' ')}] {task.Id}: {task.Description}");
            }
            
            _console.WriteLine();
        }
    }
}