namespace TaskList.Entities;

public class Task
{
    public long Id { get; }
    public string Description { get; } = string.Empty;
    public bool Done { get; set; }
    public DateOnly? DueOn { get; private set; }

    public Task(long id)
    {
        Id = id;
        Done = false;
    }
    
    public Task(long id, string description) : this(id)
    {
        Description = description;
    }

    public void SetDeadline(DateOnly deadlineDate)
    {
        DueOn = deadlineDate;
    }
}