namespace TaskList.Contracts;

public record ProjectResponse(string Name, IReadOnlyList<TaskResponse> Tasks);