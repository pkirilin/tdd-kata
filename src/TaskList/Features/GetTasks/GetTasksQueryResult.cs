using Task = TaskList.Entities.Task;

namespace TaskList.Features.GetTasks;

public record GetTasksQueryResult(IReadOnlyList<Task> Tasks);