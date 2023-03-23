using TaskList.Commands.Add;
using TaskList.Commands.Deadline;
using TaskList.Commands.Today;
using TaskList.Entities;
using TaskList.Services;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList;

public class Application
{
    private const string Quit = "quit";
    
    private readonly IConsole _console;
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;

    public Application(IConsole console, IClock clock, IProjectsService projectsService)
    {
        _console = console;
        _clock = clock;
        _projectsService = projectsService;
    }

    public void Run()
    {
        while (true)
        {
            _console.Write("> ");
            var command = _console.ReadLine() ?? Quit;
            if (command == Quit)
            {
                break;
            }

            Execute(command);
        }
    }

    private void Execute(string commandText)
    {
        var command = new Command(commandText);
        
        switch (command.Type)
        {
            case "show":
                Show();
                break;
            case "add":
                Add(command.ArgumentsText);
                break;
            case "check":
                Check(command.ArgumentsText);
                break;
            case "uncheck":
                Uncheck(command.ArgumentsText);
                break;
            case "help":
                Help();
                break;
            case "deadline":
                var request = new DeadlineRequest(command.ArgumentsText);
                var handler = new DeadlineRequestHandler(_projectsService, _console);
                handler.Handle(request);
                break;
            case "today":
                var todayCommand = new TodayCommand(_projectsService, _console);
                todayCommand.Execute();
                break;
            default:
                Error(command.Type);
                break;
        }
    }

    private void Show()
    {
        var projects = _projectsService.GetAll();
        
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

    private void Add(string? subcommandText)
    {
        var subcommand = new Command(subcommandText);

        switch (subcommand.Type)
        {
            case "project":
                AddProject(new AddProjectRequest(subcommand.ArgumentsText));
                break;
            case "task":
                AddTask(new AddTaskRequest(subcommand.ArgumentsText));
                break;
        }
    }

    private void AddProject(AddProjectRequest request)
    {
        var project = new Project(request.Name, _clock);
        _projectsService.Add(project);
    }

    private void AddTask(AddTaskRequest request)
    {
        var project = _projectsService.FindByName(request.ProjectName);

        if (project is null)
        {
            Console.WriteLine("Could not find a project with the name \"{0}\".", request.ProjectName);
            return;
        }

        var taskId = new TaskId(request.Id);
        var task = new Task(taskId, request.Description);
        project.AddTask(task);
    }

    private void Check(string? commandLineArgs)
    {
        SetDone(commandLineArgs, true);
    }

    private void Uncheck(string? commandLineArgs)
    {
        SetDone(commandLineArgs, false);
    }

    private void SetDone(string? commandLineArgs, bool done)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs))
        {
            return;
        }
        
        var taskId = new TaskId(commandLineArgs);
        var taskToUpdate = _projectsService.FindTaskById(taskId);

        if (taskToUpdate is null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", taskId);
            return;
        }

        taskToUpdate.Done = done;
    }

    private void Help()
    {
        _console.WriteLine("Commands:");
        _console.WriteLine("  show");
        _console.WriteLine("  add project <project name>");
        _console.WriteLine("  add task <project name> <task description>");
        _console.WriteLine("  check <task ID>");
        _console.WriteLine("  uncheck <task ID>");
        _console.WriteLine();
    }

    private void Error(string command)
    {
        _console.WriteLine("I don't know what the command \"{0}\" is.", command);
    }
}