using TaskList.Features;
using TaskList.Features.GetTasks;

namespace TaskList.Actions;

public class TodayAction : IAction
{
    private readonly IHandler<GetTasksQuery, GetTasksQueryResult> _getTasksHandler;
    private readonly IConsole _console;

    public TodayAction(IHandler<GetTasksQuery, GetTasksQueryResult> getTasksHandler, IConsole console)
    {
        _getTasksHandler = getTasksHandler;
        _console = console;
    }
    
    public string CommandType => "today";
    
    public void Execute(string? argumentsInputText)
    {
        var query = new GetTasksQuery
        {
            IncludeTasksOnlyDueToday = true
        };
        
        var result = _getTasksHandler.Handle(query);

        var tasksGroupedByProject = result.Tasks
            .GroupBy(t => t.Project)
            .OrderBy(t => t.Key.Name)
            .ToList()
            .AsReadOnly();

        foreach (var entry in tasksGroupedByProject)
        {
            var projectName = entry.Key.Name;
            _console.WriteLine(projectName);

            var projectTasks = entry
                .ToList()
                .AsReadOnly();
            
            foreach (var task in projectTasks)
            {
                _console.WriteLine($"    [{(task.Done ? 'x' : ' ')}] {task.Id}: {task.Description}");
            }
            
            _console.WriteLine();
        }
    }
}