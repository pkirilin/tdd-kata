using TaskList.Tests.Fakes;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class TaskBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private string _id = "";
    private string? _description;
    private DateOnly? _dueOn;

    public TaskBuilder WithId(string id)
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
        var taskId = new TaskId(_id);
        var task = new Task(taskId, _description ?? string.Empty);

        if (_dueOn.HasValue)
        {
            task.SetDeadline(_dueOn.Value);
        }
        
        return task;
    }
}