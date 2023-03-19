using Moq;
using TaskList.Commands;
using TaskList.Commands.Add;
using TaskList.Commands.Deadline;
using TaskList.Commands.Today;
using TaskList.Services;

namespace TaskList.Tests.Commands;

public class CommandFactoryTests
{
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private readonly Mock<IClock> _clockMock = new();

    [Test]
    [TestCase("add project", typeof(AddCommand))]
    [TestCase("add task", typeof(AddCommand))]
    [TestCase("deadline 1 2023-03-20", typeof(DeadlineCommand))]
    [TestCase("today", typeof(TodayCommand))]
    public void Creates_valid_command_type_based_on_command_line_input(string commandLine, Type createdCommandType)
    {
        var factory = new CommandFactory(
            commandLine,
            _projectsServiceMock.Object,
            _consoleMock.Object,
            _clockMock.Object);
        
        var command = factory.CreateCommand();
        
        Assert.That(command.GetType(), Is.EqualTo(createdCommandType));
    }
}