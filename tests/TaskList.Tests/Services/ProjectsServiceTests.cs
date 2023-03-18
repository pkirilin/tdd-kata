using TaskList.Entities;
using TaskList.Services;
using TaskList.Tests.Fakes;
using Task = TaskList.Entities.Task;

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
    public void Project_can_be_found_by_name()
    {
        var firstProject = new Project("first", _clock);
        var secondProject = new Project("second", _clock);
        _projectsService.Add(firstProject);
        _projectsService.Add(secondProject);

        var project = _projectsService.FindByName("first");
        
        Assert.That(project, Is.Not.Null);
        Assert.That(project?.Name, Is.EqualTo("first"));
    }
    
    [Test]
    public void Task_can_be_found_by_id()
    {
        var project = new Project("my project", _clock);
        var task = new Task(1, "Read a book");
        project.AddTask(task);
        _projectsService.Add(project);

        var foundTask = _projectsService.FindTaskById(1);
        
        Assert.That(foundTask, Is.Not.Null);
        Assert.That(foundTask?.Id, Is.EqualTo(1));
        Assert.That(foundTask?.Description, Is.EqualTo("Read a book"));
    }
}