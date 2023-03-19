using TaskList.Services;
using Task = TaskList.Entities.Task;

namespace TaskList.Commands.Add;

public class AddTaskCommand : ICommand
{
    private readonly IProjectsService _projectsService;

    public AddTaskCommand(string? commandLineArgs, IProjectsService projectsService)
    {
        _projectsService = projectsService;
        
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            throw new ArgumentNullException(nameof(commandLineArgs));
        }

        var tokens = commandLineArgs.Split(new[] { ' ' }, 2);
        ProjectName = tokens[0];
        Description = tokens.Length > 1 ? tokens[1] : string.Empty;
    }

    public string ProjectName { get; }
    public string Description { get; }

    public void Execute()
    {
        var project = _projectsService.FindByName(ProjectName);

        if (project is null)
        {
            Console.WriteLine("Could not find a project with the name \"{0}\".", ProjectName);
            return;
        }
        
        var taskId = _projectsService.GenerateNextTaskId();
        var task = new Task(taskId, Description);
        project.AddTask(task);
    }
}