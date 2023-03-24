using Moq;
using TaskList.DataAccess;
using TaskList.Features.SetDeadline;
using TaskList.Tests.Fakes;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class SetDeadlineQueryHandlerBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private readonly Mock<IProjectsRepository> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private TaskId _taskId = new("1");

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public SetDeadlineCommandHandler Please()
    {
        return new SetDeadlineCommandHandler(_projectsServiceMock.Object, _consoleMock.Object);
    }
    
    public SetDeadlineQueryHandlerBuilder WithTask(Task task)
    {
        _projectsServiceMock
            .Setup(x => x.FindTaskById(task.Id))
            .Returns(task);
        _taskId = task.Id;
        return this;
    }

    public SetDeadlineQueryHandlerBuilder WithNotExistingTask(string taskIdInput)
    {
        _taskId = new TaskId(taskIdInput);
        _projectsServiceMock
            .Setup(x => x.FindTaskById(_taskId))
            .Returns(null as Task);
        return this;
    }
    
    public SetDeadlineQueryHandlerBuilder WithDeadline(string deadlineDateArg)
    {
        return this;
    }
    
    public SetDeadlineQueryHandlerBuilder WithDeadlineOnToday()
    {
        Clock.CurrentDateUtc.ToString("O");
        return this;
    }
}