using Moq;
using TaskList.Features.SetDeadline;
using TaskList.Services;
using TaskList.Tests.Fakes;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class SetDeadlineRequestHandlerBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private TaskId _taskId = new("1");

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public SetDeadlineRequestHandler Please()
    {
        return new SetDeadlineRequestHandler(_projectsServiceMock.Object, _consoleMock.Object);
    }
    
    public SetDeadlineRequestHandlerBuilder WithTask(Task task)
    {
        _projectsServiceMock
            .Setup(x => x.FindTaskById(task.Id))
            .Returns(task);
        _taskId = task.Id;
        return this;
    }

    public SetDeadlineRequestHandlerBuilder WithNotExistingTask(string taskIdInput)
    {
        _taskId = new TaskId(taskIdInput);
        _projectsServiceMock
            .Setup(x => x.FindTaskById(_taskId))
            .Returns(null as Task);
        return this;
    }
    
    public SetDeadlineRequestHandlerBuilder WithDeadline(string deadlineDateArg)
    {
        return this;
    }
    
    public SetDeadlineRequestHandlerBuilder WithDeadlineOnToday()
    {
        Clock.CurrentDateUtc.ToString("O");
        return this;
    }
}