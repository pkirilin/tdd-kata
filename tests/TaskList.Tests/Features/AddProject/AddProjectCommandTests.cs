using TaskList.Features.AddProject;

namespace TaskList.Tests.Features.AddProject;

public class AddProjectCommandTests
{
    [Test]
    public void Can_be_created_from_valid_arguments()
    {
        var command = new AddProjectCommand("main");
        
        Assert.That(command.Name, Is.EqualTo("main"));
    }
}