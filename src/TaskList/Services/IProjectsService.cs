using TaskList.Entities;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Services;

public interface IProjectsService
{
    IReadOnlyList<Project> GetAll();
    
    Project? FindByName(string name);
    Task? FindTaskById(TaskId id);
    
    void Add(Project project);
}