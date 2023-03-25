using TaskList.Contracts;
using TaskList.Entities;
using Task = TaskList.Entities.Task;

namespace TaskList.Mapping;

public static class Mapper
{
    public static ProjectResponse ToProjectResponse(this Project project, IReadOnlyList<TaskResponse> taskResponses)
    {
        return new ProjectResponse(project.Name, taskResponses);
    }

    public static TaskResponse ToTaskResponse(this Task task)
    {
        return new TaskResponse(task.Id.ToString(), task.Description, task.Done);
    }
}