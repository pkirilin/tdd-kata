using TaskList.DataAccess;
using TaskList.Entities;
using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.DataAccess;

public class ProjectsRepositoryTests
{
    private readonly IProjectsRepository _projectsRepository = new InMemoryProjectsRepository();

    [Test]
    public void Project_can_be_added()
    {
        var project = new Project("my project", new FakeClock());

        _projectsRepository.Add(project);
        
        Assert.That(_projectsRepository.GetAll(), Does.Contain(project));
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
        _projectsRepository.Add(firstProject);
        _projectsRepository.Add(secondProject);

        var project = _projectsRepository.FindByName("first");
        
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
        _projectsRepository.Add(project);

        var foundTask = _projectsRepository.FindTaskById(task.Id);
        
        Assert.That(foundTask, Is.Not.Null);
        Assert.That(foundTask?.Id, Is.EqualTo(task.Id));
        Assert.That(foundTask?.Description, Is.EqualTo("Read a book"));
    }
}