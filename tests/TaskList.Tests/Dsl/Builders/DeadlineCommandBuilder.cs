using Moq;
using TaskList.Commands;
using TaskList.Services;
using TaskList.Tests.Fakes;
using Task = TaskList.Entities.Task;

namespace TaskList.Tests.Dsl.Builders;

public class DeadlineCommandBuilder
{
    private static readonly IClock Clock = new FakeClock();
    
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private string _taskIdArg = "1";
    private string _deadlineDateArg = "2023-03-01";

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public DeadlineCommand Please()
    {
        return new DeadlineCommand(
            $"{_taskIdArg} {_deadlineDateArg}",
            _projectsServiceMock.Object,
            _consoleMock.Object);
    }
    
    public DeadlineCommandBuilder WithTask(Task task)
    {
        _projectsServiceMock
            .Setup(x => x.FindTaskById(task.Id))
            .Returns(task);
        _taskIdArg = task.Id.ToString();
        return this;
    }
    
    public DeadlineCommandBuilder WithTaskId(string taskIdArg)
    {
        _taskIdArg = taskIdArg;
        return this;
    }
    
    public DeadlineCommandBuilder WithNotExistingTask(long taskId)
    {
        _projectsServiceMock
            .Setup(x => x.FindTaskById(taskId))
            .Returns(null as Task);
        _taskIdArg = taskId.ToString();
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