using TaskList.Features.AddProject;
using TaskList.Features.AddTask;
using TaskList.Features.SetDeadline;
using TaskList.Features.ShowTasksDueToday;
using TaskList.Services;
using TaskList.ValueObjects;

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
                var request = new SetDeadlineCommand(command.ArgumentsText);
                var handler = new SetDeadlineCommandHandler(_projectsService, _console);
                handler.Handle(request);
                break;
            case "today":
                var todayCommand = new ShowTasksDueTodayQueryHandler(_projectsService, _console);
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
                var addProjectHandler = new AddProjectCommandHandler(_clock, _projectsService);
                addProjectHandler.Handle(new AddProjectCommand(subcommand.ArgumentsText));
                break;
            case "task":
                var addTaskHandler = new AddTaskCommandHandler(_projectsService);
                addTaskHandler.Handle(new AddTaskCommand(subcommand.ArgumentsText));
                break;
        }
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