namespace TaskList.Features.GetTasks;

public class GetTasksQuery
{
    public bool IncludeTasksOnlyWithDueDate { get; init; }
    public bool IncludeTasksOnlyDueToday { get; init; }
}