using TaskList.Commands.Add;

namespace TaskList.Tests.Commands.Add;

public class AddCommandFactoryTests
{
    [Test]
    [TestCase("project my_project", typeof(AddProjectCommand))]
    [TestCase("task my_project Read a book", typeof(AddTaskCommand))]
    public void Creates_valid_command_type_based_on_command_line_input(string commandLine, Type createdCommandType)
    {
        var factory = new AddCommandFactory(commandLine);

        var command = factory.CreateCommand();
        
        Assert.That(command.GetType(), Is.EqualTo(createdCommandType));
    }
}