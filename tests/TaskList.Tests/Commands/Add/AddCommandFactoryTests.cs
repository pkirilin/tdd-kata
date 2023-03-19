using Moq;
using TaskList.Commands.Add;
using TaskList.Services;

namespace TaskList.Tests.Commands.Add;

public class AddCommandFactoryTests
{
    private readonly Mock<IClock> _clockMock = new();
    private readonly Mock<IProjectsService> _projectsServiceMock = new();

    [Test]
    [TestCase("project my_project", typeof(AddProjectCommand))]
    [TestCase("task my_project Read a book", typeof(AddTaskCommand))]
    public void Creates_valid_command_type_based_on_command_line_input(string commandLine, Type createdCommandType)
    {
        var factory = new AddCommandFactory(commandLine, _clockMock.Object, _projectsServiceMock.Object);

        var command = factory.CreateCommand();
        
        Assert.That(command.GetType(), Is.EqualTo(createdCommandType));
    }
}