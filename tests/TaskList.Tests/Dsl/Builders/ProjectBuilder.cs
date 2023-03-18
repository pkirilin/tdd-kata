using TaskList.Entities;
using TaskList.Tests.Fakes;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class ProjectBuilder
{
    private static readonly IClock Clock = new FakeClock();
    private string _name = "default";
    private readonly List<Task> _tasks = new();

    public Project Please()
    {
        var project = new Project(_name, Clock);

        foreach (var task in _tasks)
        {
            project.AddTask(task);
        }
        
        return project;
    }

    public ProjectBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProjectBuilder WithTasks(params Task[] tasks)
    {
        _tasks.AddRange(tasks);
        return this;
    }
}