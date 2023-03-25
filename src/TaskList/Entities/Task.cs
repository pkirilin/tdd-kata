using TaskList.ValueObjects;

namespace TaskList.Entities;

public class Task
{
    public TaskId Id { get; }
    public string Description { get; } = string.Empty;
    public bool Done { get; set; }
    public DateOnly? DueOn { get; private set; }
    public Project Project { get; }

    public Task(TaskId id, Project project)
    {
        Id = id;
        Project = project;
        Done = false;
    }
    
    public Task(TaskId id, Project project, string description) : this(id, project)
    {
        Description = description;
    }

    public void SetDeadline(DateOnly deadlineDate)
    {
        DueOn = deadlineDate;
    }
}