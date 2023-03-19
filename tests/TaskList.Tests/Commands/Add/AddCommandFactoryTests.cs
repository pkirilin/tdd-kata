using Moq;
using TaskList.Commands.Add;
using TaskList.Services;
using TaskList.ValueObjects;

namespace TaskList.Tests.Commands.Add;

public class AddCommandFactoryTests
{
    private readonly Mock<IClock> _clockMock = new();
    private readonly Mock<IProjectsService> _projectsServiceMock = new();

    [Test]
    [TestCase("project my_project", 1, typeof(AddProjectCommand))]
    [TestCase("task my_project Read a book", 2, typeof(AddTaskCommand))]
    public void Creates_valid_command_type_based_on_command_line_input(string inputText, int argumentsCount, Type createdCommandType)
    {
        var commandText = new CommandText(inputText, argumentsCount);
        var factory = new AddCommandFactory(commandText, _clockMock.Object, _projectsServiceMock.Object);

        var command = factory.CreateCommand();
        
        Assert.That(command.GetType(), Is.EqualTo(createdCommandType));
    }
}