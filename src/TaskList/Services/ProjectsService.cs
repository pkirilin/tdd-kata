using TaskList.Entities;

namespace TaskList.Services;

public class ProjectsService : IProjectsService
{
    private readonly List<Project> _projects = new();

    public IReadOnlyList<Project> GetAll()
    {
        return _projects;
    }

    public Project? GetByName(string name)
    {
        return _projects.FirstOrDefault(p => p.Name == name);
    }

    public void Add(Project project)
    {
        _projects.Add(project);
    }
}