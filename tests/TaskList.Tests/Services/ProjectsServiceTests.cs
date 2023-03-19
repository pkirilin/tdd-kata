using TaskList.Entities;
using TaskList.Services;
using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Services;

public class ProjectsServiceTests
{
    private readonly IProjectsService _projectsService = new ProjectsService();

    [Test]
    public void Project_can_be_added()
    {
        var project = new Project("my project", new FakeClock());

        _projectsService.Add(project);
        
        Assert.That(_projectsService.GetAll(), Does.Contain(project));
    }

    [Test]
    public void Project_can_be_found_by_name()
    {
        var firstProject = Create
            .Project()
            .WithName("first")
            .Please();
        var secondProject = Create
            .Project()
            .WithName("second")
            .Please();
        _projectsService.Add(firstProject);
        _projectsService.Add(secondProject);

        var project = _projectsService.FindByName("first");
        
        Assert.That(project, Is.Not.Null);
        Assert.That(project?.Name, Is.EqualTo("first"));
    }
    
    [Test]
    public void Task_can_be_found_by_id()
    {
        var task = Create
            .Task()
            .WithId("1")
            .WithDescription("Read a book")
            .Please();
        var project = Create
            .Project()
            .WithName("my project")
            .WithTasks(task)
            .Please();
        _projectsService.Add(project);

        var foundTask = _projectsService.FindTaskById(task.Id);
        
        Assert.That(foundTask, Is.Not.Null);
        Assert.That(foundTask?.Id, Is.EqualTo(task.Id));
        Assert.That(foundTask?.Description, Is.EqualTo("Read a book"));
    }
}