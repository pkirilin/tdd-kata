using TaskList.Features.GetTasksDueToday;
using TaskList.Tests.Dsl;

namespace TaskList.Tests.Features.GetTasksDueToday;

public class GetTasksDueTodayQueryTests
{
    [Test]
    public void Returns_tasks_due_today_when_executed()
    {
        var project1 = Create
            .Project()
            .WithName("project1")
            .WithTasksHavingDeadlineOnToday("Wash the dishes")
            .WithTasks("Read the book")
            .Please();
        var project2 = Create
            .Project()
            .WithName("project2")
            .WithTasksHavingDeadlineOnToday("Go for a walk")
            .Please();
        var project3 = Create
            .Project()
            .WithName("project3")
            .WithTasks("Play with cat", "Call a friend")
            .Please();
        var handler = Create
            .ShowTasksDueTodayQueryHandler()
            .WithProjects(project1, project2, project3)
            .Please();
        
        var result = handler.Handle(new GetTasksDueTodayQuery());
        
        var resultProjectNames = result.Projects
            .Select(p => p.Name)
            .ToList()
            .AsReadOnly();
        var resultTaskDescriptions = result.Projects
            .SelectMany(p => p.Tasks.Select(t => t.Description))
            .ToList()
            .AsReadOnly();
        
        Assert.That(resultProjectNames, Is.EqualTo(new[]
        {
            project1.Name,
            project2.Name
        }));
        Assert.That(resultTaskDescriptions, Is.EqualTo(new[]
        {
            "Wash the dishes",
            "Go for a walk"
        }));
    }
}