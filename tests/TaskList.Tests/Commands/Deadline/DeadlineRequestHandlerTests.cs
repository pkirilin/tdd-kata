using System.Text.RegularExpressions;
using Moq;
using TaskList.Commands.Deadline;
using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Commands.Deadline;

public class DeadlineRequestHandlerTests
{
    private static readonly IClock Clock = new FakeClock();

    [Test]
    public void Sets_due_date_for_existing_task_when_executed_with_valid_arguments()
    {
        var task = Create
            .Task()
            .WithId("123")
            .Please();
        var request = new DeadlineRequest($"{task.Id} 2023-03-01");
        var handler = Create
            .DeadlineRequestHandler()
            .WithTask(task)
            .Please();

        handler.Handle(request);
        
        Assert.That(task.DueOn, Is.EqualTo(request.Date));
    }

    [Test]
    public void Cannot_set_due_date_for_not_existing_task()
    {
        var request = new DeadlineRequest($"123 {Clock.CurrentDateUtc.ToString("o")}");
        var handlerBuilder = Create
            .DeadlineRequestHandler()
            .WithNotExistingTask("123");
        var consoleMock = handlerBuilder.ConsoleMock;
        var handler = handlerBuilder.Please();

        handler.Handle(request);

        consoleMock.Verify(
            x => x.WriteLine(It.IsRegex("could not find a task with an ID of", RegexOptions.IgnoreCase)),
            Times.Once);
    }
}