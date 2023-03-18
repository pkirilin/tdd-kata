using TaskList.Entities;

namespace TaskList.Services;

public class ProjectsService : IProjectsService
{
    private readonly List<Project> _projects = new();

    public IReadOnlyList<Project> GetAll()
    {
        return _projects;
    }

    public void Add(Project project)
    {
        _projects.Add(project);
    }
}