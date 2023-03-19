using Moq;
using TaskList.Commands;
using TaskList.Services;
using TaskList.Tests.Fakes;
using TaskList.ValueObjects;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class DeadlineCommandBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private TaskId _taskId = new("1");
    private string _deadlineDateArg = "2023-03-01";

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public DeadlineCommand Please()
    {
        return new DeadlineCommand(
            $"{_taskId} {_deadlineDateArg}",
            _projectsServiceMock.Object,
            _consoleMock.Object);
    }
    
    public DeadlineCommandBuilder WithTask(Task task)
    {
        _projectsServiceMock
            .Setup(x => x.FindTaskById(task.Id))
            .Returns(task);
        _taskId = task.Id;
        return this;
    }
    
    public DeadlineCommandBuilder WithTaskId(string taskIdInput)
    {
        _taskId = new TaskId(taskIdInput);
        return this;
    }
    
    public DeadlineCommandBuilder WithNotExistingTask(string taskIdInput)
    {
        _taskId = new TaskId(taskIdInput);
        _projectsServiceMock
            .Setup(x => x.FindTaskById(_taskId))
            .Returns(null as Task);
        return this;
    }
    
    public DeadlineCommandBuilder WithDeadline(string deadlineDateArg)
    {
        _deadlineDateArg = deadlineDateArg;
        return this;
    }
    
    public DeadlineCommandBuilder WithDeadlineOnToday()
    {
        _deadlineDateArg = Clock.CurrentDateUtc.ToString("O");
        return this;
    }
}