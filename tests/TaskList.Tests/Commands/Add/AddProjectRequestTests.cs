using TaskList.Commands.Add;

namespace TaskList.Tests.Commands.Add;

public class AddProjectRequestTests
{
    [Test]
    public void Can_be_created_from_valid_arguments()
    {
        var request = new AddProjectRequest("main");
        
        Assert.That(request.Name, Is.EqualTo("main"));
    }
}