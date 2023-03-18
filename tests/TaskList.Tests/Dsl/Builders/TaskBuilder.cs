using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class TaskBuilder
{
    private long _nextId;
    private string? _description;
    private DateOnly? _dueOn;

    public TaskBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public TaskBuilder WithDeadlineOnToday(IClock clock)
    {
        _dueOn = clock.CurrentDateUtc;
        return this;
    }
    
    public Task Please()
    {
        var task = new Task(++_nextId, _description ?? string.Empty);

        if (_dueOn.HasValue)
        {
            task.SetDeadline(_dueOn.Value);
        }
        
        return task;
    }
}