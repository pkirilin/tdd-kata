using TaskList.Commands;
using TaskList.Entities;
using TaskList.Services;
using Task = TaskList.Entities.Task;

namespace TaskList;

public class Application
{
    private const string Quit = "quit";
    
    private readonly IConsole _console;
    private readonly IClock _clock;
    private readonly IProjectsService _projectsService;
    private long _lastId;

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
            var command = _console.ReadLine();
            if (command == Quit)
            {
                break;
            }

            Execute(command);
        }
    }

    private void Execute(string commandLine)
    {
        var commandRest = commandLine.Split(" ".ToCharArray(), 2);
        var command = commandRest[0];
        switch (command)
        {
            case "show":
                Show();
                break;
            case "add":
                Add(commandRest[1]);
                break;
            case "check":
                Check(commandRest[1]);
                break;
            case "uncheck":
                Uncheck(commandRest[1]);
                break;
            case "help":
                Help();
                break;
            case "deadline":
                var deadlineCommand = new DeadlineCommand(commandRest[1], _projectsService, _console);
                deadlineCommand.Execute();
                break;
            case "today":
                ShowTasksDueToday();
                break;
            default:
                Error(command);
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

    private void Add(string commandLine)
    {
        var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
        var subcommand = subcommandRest[0];
        if (subcommand == "project")
        {
            AddProject(subcommandRest[1]);
        }
        else if (subcommand == "task")
        {
            var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
            AddTask(projectTask[0], projectTask[1]);
        }
    }

    private void AddProject(string name)
    {
        var project = new Project(name, _clock);
        _projectsService.Add(project);
    }

    private void AddTask(string projectName, string description)
    {
        var project = _projectsService.FindByName(projectName);

        if (project is null)
        {
            Console.WriteLine("Could not find a project with the name \"{0}\".", projectName);
            return;
        }

        var task = new Task(NextId(), description);
        project.AddTask(task);
    }

    private void Check(string idString)
    {
        SetDone(idString, true);
    }

    private void Uncheck(string idString)
    {
        SetDone(idString, false);
    }

    private void SetDone(string idString, bool done)
    {
        var id = long.Parse(idString);
        var taskToUpdate = _projectsService.FindTaskById(id);

        if (taskToUpdate is null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", id);
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

    private void ShowTasksDueToday()
    {
        var projects = _projectsService.GetAll();
        
        foreach (var project in projects)
        {
            var tasksDueToday = project.GetTasksDueToday();

            if (!tasksDueToday.Any())
            {
                continue;
            }
            
            _console.WriteLine(project.Name);
                
            foreach (var task in tasksDueToday)
            {
                _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
            }
                
            _console.WriteLine();
        }
    }

    private void Error(string command)
    {
        _console.WriteLine("I don't know what the command \"{0}\" is.", command);
    }

    private long NextId()
    {
        return ++_lastId;
    }
}