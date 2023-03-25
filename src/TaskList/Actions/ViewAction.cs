using TaskList.Features;
using TaskList.Features.GetTasks;

namespace TaskList.Actions;

public class ViewAction : IAction
{
    private readonly IConsole _console;
    private readonly IHandler<GetTasksQuery, GetTasksQueryResult> _getTasksHandler;

    public ViewAction(IConsole console, IHandler<GetTasksQuery, GetTasksQueryResult> getTasksHandler)
    {
        _console = console;
        _getTasksHandler = getTasksHandler;
    }
    
    public string CommandType => "view";
    
    public void Execute(string? argumentsInputText)
    {
        var query = new GetTasksQuery
        {
            IncludeTasksOnlyWithDueDate = true
        };
        
        var result = _getTasksHandler.Handle(query);

        var tasksGroupedByDueDate = result.Tasks
            .GroupBy(t => t.DueOn)
            .OrderBy(g => g.Key)
            .ToList()
            .AsReadOnly();
        
        foreach (var group in tasksGroupedByDueDate)
        {
            var date = group.Key?.ToString("o") ?? "<no date>";
            _console.WriteLine(date);
            
            var tasks = group
                .ToList()
                .AsReadOnly();
            
            foreach (var task in tasks)
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
            }
        
            _console.WriteLine();
        }
    }
}