using TaskList.Entities;

namespace TaskList.Services;

public interface IProjectsService
{
    IReadOnlyList<Project> GetAll();
    
    Project? GetByName(string name);
    
    void Add(Project project);
}