using TaskList.Features.AddProject;

namespace TaskList.Tests.Features.AddProject;

public class AddProjectRequestTests
{
    [Test]
    public void Can_be_created_from_valid_arguments()
    {
        var request = new AddProjectRequest("main");
        
        Assert.That(request.Name, Is.EqualTo("main"));
    }
}