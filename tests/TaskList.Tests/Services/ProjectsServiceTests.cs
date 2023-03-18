using TaskList.Entities;
using TaskList.Services;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Services;

public class ProjectsServiceTests
{
    private readonly IClock _clock = new FakeClock();
    private readonly IProjectsService _projectsService = new ProjectsService();

    [Test]
    public void Project_can_be_added()
    {
        var project = new Project("my project", new FakeClock());

        _projectsService.Add(project);
        
        Assert.That(_projectsService.GetAll(), Does.Contain(project));
    }

    [Test]
    public void Project_can_be_received_by_name()
    {
        var firstProject = new Project("first", _clock);
        var secondProject = new Project("second", _clock);
        _projectsService.Add(firstProject);
        _projectsService.Add(secondProject);

        var project = _projectsService.GetByName("first");
        
        Assert.That(project, Is.Not.Null);
        Assert.That(project?.Name, Is.EqualTo("first"));
    }
}