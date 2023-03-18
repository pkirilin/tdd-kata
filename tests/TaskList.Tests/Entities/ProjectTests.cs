using TaskList.Entities;
using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Entities;

public class ProjectTests
{
    [Test]
    public void Task_can_be_added_to_existing_project()
    {
        var project = new Project("my project", new FakeClock());
        var task = Create.Task().Please();
        
        project.AddTask(task);
        
        Assert.That(project.Tasks, Contains.Item(task));
    }
    
    [Test]
    public void All_tasks_due_today_are_received()
    {
        var project = new Project("my project", new FakeClock());

        var task1 = Create.Task()
            .WithDeadlineOnToday()
            .Please();
        var task2 = Create.Task().Please();

        project.AddTask(task1);
        project.AddTask(task2);

        var tasksDueToday = project.GetTasksDueToday();
        
        Assert.That(tasksDueToday, Does.Contain(task1));
        Assert.That(tasksDueToday, Does.Not.Contain(task2));
    }
}