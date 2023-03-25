using TaskList.Entities;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.DataAccess;

public class InMemoryProjectsRepository : IProjectsRepository
{
    private readonly List<Project> _projects = new();

    public IReadOnlyList<Project> GetAll()
    {
        return _projects;
    }

    public Project? FindByName(string name)
    {
        return _projects.FirstOrDefault(p => p.Name == name);
    }

    public Project? FindByTaskId(TaskId id)
    {
        return _projects.FirstOrDefault(p => p.Tasks.Any(t => t.Id == id));
    }

    public Task? FindTaskById(TaskId id)
    {
        return _projects
            .SelectMany(p => p.Tasks)
            .FirstOrDefault(t => t.Id == id);
    }

    public void Add(Project project)
    {
        _projects.Add(project);
    }
}