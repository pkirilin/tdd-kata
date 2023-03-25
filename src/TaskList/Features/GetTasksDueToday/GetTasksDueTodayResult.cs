using TaskList.Contracts;

namespace TaskList.Features.GetTasksDueToday;

public record GetTasksDueTodayResult(IReadOnlyList<ProjectResponse> Projects);