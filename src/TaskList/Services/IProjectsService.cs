using TaskList.Entities;
using Task = TaskList.Entities.Task;

namespace TaskList.Services;

public interface IProjectsService
{
    IReadOnlyList<Project> GetAll();
    
    Project? FindByName(string name);
    Task? FindTaskById(long id);
    
    void Add(Project project);
}