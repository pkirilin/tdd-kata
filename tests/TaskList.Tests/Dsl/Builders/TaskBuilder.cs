using TaskList.Tests.Fakes;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class TaskBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private long _id = 1;
    private string? _description;
    private DateOnly? _dueOn;

    public TaskBuilder WithId(long id)
    {
        _id = id;
        return this;
    }

    public TaskBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public TaskBuilder WithDeadlineOnToday()
    {
        _dueOn = Clock.CurrentDateUtc;
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