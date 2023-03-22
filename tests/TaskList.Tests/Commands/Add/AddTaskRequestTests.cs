using TaskList.Commands.Add;

namespace TaskList.Tests.Commands.Add;

public class AddTaskRequestTests
{
    [Test]
    public void Can_be_created_from_valid_arguments()
    {
        var request = new AddTaskRequest("main Read a book");
        
        Assert.That(request.ProjectName, Is.EqualTo("main"));
        Assert.That(request.Description, Is.EqualTo("Read a book"));
    }
}