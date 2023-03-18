using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class TaskBuilder
{
    private long _id = 1;
    private readonly string? _description = null!;
    private DateOnly? _dueOn;

    public TaskBuilder WithId(long id)
    {
        _id = id;
        return this;
    }

    public TaskBuilder WithDeadlineOnToday(IClock clock)
    {
        _dueOn = clock.CurrentDateUtc;
        return this;
    }
    
    public Task Please()
    {
        var task = new Task(_id, _description ?? string.Empty);

        if (_dueOn.HasValue)
        {
            task.SetDeadline(_dueOn.Value);
        }
        
        return task;
    }
}