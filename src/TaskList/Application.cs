namespace TaskList;

public class Application
{
    private const string Quit = "quit";

    private readonly IDictionary<string, IList<Task>> _tasks = new Dictionary<string, IList<Task>>();
    private readonly IConsole _console;
    private readonly IClock _clock;

    private long _lastId = 0;

    public Application(IConsole console, IClock clock)
    {
        _console = console;
        _clock = clock;
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
                var deadlineOptions = commandRest[1]
                        .Split(' ')
                        .ToArray();
                SetDeadline(deadlineOptions[0], deadlineOptions[1]);
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
        foreach (var project in _tasks)
        {
            _console.WriteLine(project.Key);
            foreach (var task in project.Value)
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
        _tasks[name] = new List<Task>();
    }

    private void AddTask(string project, string description)
    {
        if (!_tasks.TryGetValue(project, out IList<Task> projectTasks))
        {
            Console.WriteLine("Could not find a project with the name \"{0}\".", project);
            return;
        }

        projectTasks.Add(new Task { Id = NextId(), Description = description, Done = false });
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
        int id = int.Parse(idString);
        var identifiedTask = _tasks
            .Select(project => project.Value.FirstOrDefault(task => task.Id == id))
            .Where(task => task != null)
            .FirstOrDefault();
        if (identifiedTask == null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", id);
            return;
        }

        identifiedTask.Done = done;
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

    private void SetDeadline(string taskId, string deadlineDateRaw)
    {
        var id = int.Parse(taskId);
        var identifiedTask = _tasks
            .Select(project => project.Value.FirstOrDefault(task => task.Id == id))
            .FirstOrDefault(task => task != null);
        
        if (identifiedTask == null)
        {
            _console.WriteLine("Could not find a task with an ID of {0}.", id);
            return;
        }

        if (!DateOnly.TryParse(deadlineDateRaw, out var deadlineDate))
        {
            _console.WriteLine($"Could not parse deadline date {deadlineDate}.");
            return;
        }

        identifiedTask.DueOn = deadlineDate;
    }

    private void ShowTasksDueToday()
    {
        var todayDate = _clock.CurrentDateUtc;

        foreach (var project in _tasks)
        {
            if (project.Value.Any(task => task.DueOn == todayDate))
            {
                _console.WriteLine(project.Key);
                foreach (var task in project.Value)
                {
                    if (task.DueOn == todayDate)
                    {
                        _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
                    }
                }

                _console.WriteLine();
            }
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