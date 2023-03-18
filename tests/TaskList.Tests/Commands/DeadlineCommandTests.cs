using System.Text.RegularExpressions;
using Moq;
using TaskList.Tests.Dsl;

namespace TaskList.Tests.Commands;

public class DeadlineCommandTests
{
    [Test]
    public void Sets_due_date_for_existing_task_when_executed_with_valid_arguments()
    {
        var task = Create
            .Task()
            .WithId(123)
            .Please();

        var command = Create
            .DeadlineCommand()
            .WithTask(task)
            .WithDeadlineOnToday()
            .Please();
        
        command.Execute();
        
        Assert.That(task.DueOn, Is.EqualTo(command.DeadlineDate));
    }

    [Test]
    public void Cannot_set_due_date_for_not_existing_task()
    {
        var commandBuilder = Create
            .DeadlineCommand()
            .WithNotExistingTask(123)
            .WithDeadlineOnToday();
        var consoleMock = commandBuilder.ConsoleMock;
        var command = commandBuilder.Please();
        
        command.Execute();
        
        consoleMock.Verify(
            x => x.WriteLine(It.IsRegex("could not find a task with an ID of", RegexOptions.IgnoreCase)),
            Times.Once);
    }

    [Test]
    public void Cannot_be_created_with_invalid_task_id()
    {
        Assert.Throws<FormatException>(() =>
        {
            Create
                .DeadlineCommand()
                .WithTaskId("adasdasd")
                .Please();
        });
    }
    
    [Test]
    public void Cannot_be_created_with_invalid_deadline_date()
    {
        Assert.Throws<FormatException>(() =>
        {
            Create
                .DeadlineCommand()
                .WithDeadline("vsvsvs")
                .Please();
        });
    }
}