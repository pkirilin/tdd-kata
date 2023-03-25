namespace TaskList.Entities;

public class Project
{
    private readonly IClock _clock;
    private readonly List<Task> _tasks = new();
    
    public string Name { get; }
    public IReadOnlyList<Task> Tasks => _tasks;

    public Project(string name, IClock clock)
    {
        _clock = clock;
        Name = name;
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }

    public void RemoveTask(Task task)
    {
        _tasks.Remove(task);
    }
    
    public IReadOnlyList<Task> GetTasksDueToday()
    {
        return _tasks
            .Where(t => t.DueOn == _clock.CurrentDateUtc)
            .ToList();
    }
}