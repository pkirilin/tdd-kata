using TaskList.Entities;

namespace TaskList.Services;

public interface IProjectsService
{
    IReadOnlyList<Project> GetAll();
    
    void Add(Project project);
}