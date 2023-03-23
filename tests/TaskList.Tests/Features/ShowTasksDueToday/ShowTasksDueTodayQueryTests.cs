using Moq;
using TaskList.Tests.Dsl;

namespace TaskList.Tests.Features.ShowTasksDueToday;

public class ShowTasksDueTodayQueryTests
{
    [Test]
    public void Prints_tasks_due_today_when_executed()
    {
        var task1 = Create
            .Task()
            .WithDescription("Wash the dishes")
            .WithDeadlineOnToday()
            .Please();
        var task2 = Create
            .Task()
            .WithDescription("Read the book")
            .Please();
        var project = Create
            .Project()
            .WithName("my project")
            .WithTasks(task1, task2)
            .Please();
        var commandBuilder = Create
            .ShowTasksDueTodayQueryHandler()
            .WithProjects(project);
        var consoleMock = commandBuilder.ConsoleMock;
        var command = commandBuilder.Please();
        
        command.Handle();
        
        consoleMock.Verify(x => x.WriteLine(It.IsRegex(project.Name)), Times.Once);
        consoleMock.Verify(x => x.WriteLine(It.Is<string>(line => line.Contains(task1.Description))), Times.Once);
        consoleMock.Verify(x => x.WriteLine(It.Is<string>(line => line.Contains(task2.Description))), Times.Never);
    }
}