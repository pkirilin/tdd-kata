using TaskList.Features.GetTasks;
using TaskList.Tests.Dsl;

namespace TaskList.Tests.Features.GetTasks;

public class GetTasksQueryHandlerTests
{
    [Test]
    public void Filters_out_tasks_without_due_date()
    {
        var project1 = Create
            .Project()
            .WithName("first")
            .WithTasks("Go for a walk")
            .WithTaskHavingDeadline("Read a book", "2023-03-20")
            .WithTaskHavingDeadline("Play with cat", "2023-03-21")
            .Please();
        var project2 = Create
            .Project()
            .WithName("second")
            .WithTasks("Wash the dishes", "Call a friend")
            .WithTaskHavingDeadline("Work", "2023-03-21")
            .Please();
        var handler = Create
            .GetTasksQueryHandler()
            .WithProjects(project1, project2)
            .Please();
        var query = new GetTasksQuery
        {
            IncludeTasksOnlyWithDueDate = true
        };

        var result = handler.Handle(query);
        var resultTaskDescriptions = result.Tasks
            .Select(t => t.Description)
            .ToList()
            .AsReadOnly();

        Assert.That(resultTaskDescriptions, Is.EqualTo(new[]
        {
            "Read a book",
            "Play with cat",
            "Work"
        }));
    }
}