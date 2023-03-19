using TaskList.ValueObjects;

namespace TaskList.Entities;

public class Task
{
    public TaskId Id { get; }
    public string Description { get; } = string.Empty;
    public bool Done { get; set; }
    public DateOnly? DueOn { get; private set; }

    public Task(TaskId id)
    {
        Id = id;
        Done = false;
    }
    
    public Task(TaskId id, string description) : this(id)
    {
        Description = description;
    }

    public void SetDeadline(DateOnly deadlineDate)
    {
        DueOn = deadlineDate;
    }
}