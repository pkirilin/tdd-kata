using TaskList.Features.AddTask;

namespace TaskList.Tests.Features.AddTask;

public class AddTaskRequestTests
{
    [Test]
    public void Accepts_project_name_with_task_description_and_string_id()
    {
        var request = new AddTaskRequest("main fdsfsd1 Read a book");
        
        Assert.That(request.ProjectName, Is.EqualTo("main"));
        Assert.That(request.Id, Is.EqualTo("fdsfsd1"));
        Assert.That(request.Description, Is.EqualTo("Read a book"));
    }
}