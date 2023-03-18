using TaskList.Entities;
using TaskList.Services;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Services;

public class ProjectsServiceTests
{
    private IProjectsService _projectsService = null!;
    
    [SetUp]
    public void SetUp()
    {
        _projectsService = new ProjectsService();
    }
    
    [Test]
    public void Project_can_be_added()
    {
        var project = new Project("my project", new FakeClock());

        _projectsService.Add(project);
        
        Assert.That(_projectsService.GetAll(), Does.Contain(project));
    }
}