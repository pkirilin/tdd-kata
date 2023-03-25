using TaskList.ValueObjects;

namespace TaskList.Features.DeleteTask;

public record DeleteTaskCommand(TaskId Id);