using TaskList.Features;
using TaskList.Features.GetTasks;

namespace TaskList.Actions;

public class ShowAction : IAction
{
    private readonly IHandler<GetTasksQuery, GetTasksQueryResult> _getTasksHandler;
    private readonly IConsole _console;

    public ShowAction(IHandler<GetTasksQuery, GetTasksQueryResult> getTasksHandler, IConsole console)
    {
        _getTasksHandler = getTasksHandler;
        _console = console;
    }
    
    public string CommandType => "show";
    
    public void Execute(string? argumentsInputText)
    {
        var query = new GetTasksQuery();
        var result = _getTasksHandler.Handle(query);

        var projects = result.Tasks
            .GroupBy(t => t.Project)
            .Select(g => g.Key)
            .ToList()
            .AsReadOnly();
        
        foreach (var project in projects)
        {
            _console.WriteLine(project.Name);
            
            foreach (var task in project.Tasks)
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
            }

            _console.WriteLine();
        }
    }
}