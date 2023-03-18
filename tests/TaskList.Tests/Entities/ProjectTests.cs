using TaskList.Entities;
using TaskList.Tests.Fakes;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Entities;

public class ProjectTests
{
    [Test]
    public void Task_can_be_added_to_existing_project()
    {
        var project = new Project("my project", new FakeClock());
        var task = new Task
        {
            Id = 1,
            Description = "First",
            DueOn = DateOnly.Parse("2023-03-20")
        };
        
        project.AddTask(task);
        
        Assert.That(project.Tasks, Contains.Item(task));
    }
    
    [Test]
    public void All_tasks_due_today_are_received()
    {
        var project = new Project("my project", new FakeClock());
        
        var task1 = new Task
        {
            Id = 1,
            Description = "First",
            DueOn = DateOnly.Parse("2023-03-20")
        };
        
        var task2 = new Task
        {
            Id = 2,
            Description = "Second",
            DueOn = DateOnly.Parse("2023-03-21")
        };
        
        project.AddTask(task1);
        project.AddTask(task2);

        var tasksDueToday = project.GetTasksDueToday();
        
        Assert.That(tasksDueToday, Does.Contain(task1));
        Assert.That(tasksDueToday, Does.Not.Contain(task2));
    }
}