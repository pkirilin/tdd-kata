using System.Text.RegularExpressions;
using Moq;
using TaskList.Features.SetDeadline;
using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Features.SetDeadline;

public class SetDeadlineQueryHandlerTests
{
    private static readonly IClock Clock = new FakeClock();

    [Test]
    public void Sets_due_date_for_existing_task_when_executed_with_valid_arguments()
    {
        var task = Create
            .Task()
            .WithId("123")
            .Please();
        var query = new SetDeadlineCommand($"{task.Id} 2023-03-01");
        var handler = Create
            .SetDeadlineQueryHandler()
            .WithTask(task)
            .Please();

        handler.Handle(query);
        
        Assert.That(task.DueOn, Is.EqualTo(query.Date));
    }

    [Test]
    public void Cannot_set_due_date_for_not_existing_task()
    {
        var query = new SetDeadlineCommand($"123 {Clock.CurrentDateUtc.ToString("o")}");
        var handlerBuilder = Create
            .SetDeadlineQueryHandler()
            .WithNotExistingTask("123");
        var consoleMock = handlerBuilder.ConsoleMock;
        var handler = handlerBuilder.Please();

        handler.Handle(query);

        consoleMock.Verify(
            x => x.WriteLine(It.IsRegex("could not find a task with an ID of", RegexOptions.IgnoreCase)),
            Times.Once);
    }
}