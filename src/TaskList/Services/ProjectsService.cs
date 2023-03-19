using TaskList.Entities;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Services;

public class ProjectsService : IProjectsService
{
    private long _lastGeneratedTaskId;
    private readonly List<Project> _projects = new();

    public TaskId GenerateNextTaskId()
    {
        return new TaskId((++_lastGeneratedTaskId).ToString());
    }

    public IReadOnlyList<Project> GetAll()
    {
        return _projects;
    }

    public Project? FindByName(string name)
    {
        return _projects.FirstOrDefault(p => p.Name == name);
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