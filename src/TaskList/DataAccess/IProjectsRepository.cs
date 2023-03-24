using TaskList.Entities;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.DataAccess;

public interface IProjectsRepository
{
    IReadOnlyList<Project> GetAll();
    
    Project? FindByName(string name);
    
    Task? FindTaskById(TaskId id);
    
    void Add(Project project);
}