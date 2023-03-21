using Moq;
using TaskList.Commands;
using TaskList.Commands.Add;
using TaskList.Commands.Today;
using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Tests.Commands;

public class CommandFactoryTests
{
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();
    private readonly Mock<IClock> _clockMock = new();

    [Test]
    [TestCase("add project main", 2, typeof(AddCommand))]
    [TestCase("add task 1 test task", 3, typeof(AddCommand))]
    [TestCase("today", 0, typeof(TodayCommand))]
    public void Creates_valid_command_type_based_on_command_line_input(string inputText, int argumentsCount, Type createdCommandType)
    {
        var commandText = new CommandText(inputText, argumentsCount);
        var factory = new CommandFactory(
            commandText,
            _projectsServiceMock.Object,
            _consoleMock.Object,
            _clockMock.Object);
        
        var command = factory.CreateCommand();
        
        Assert.That(command.GetType(), Is.EqualTo(createdCommandType));
    }
}