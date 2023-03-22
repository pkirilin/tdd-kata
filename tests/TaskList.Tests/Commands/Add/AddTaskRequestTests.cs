using TaskList.Commands.Add;

namespace TaskList.Tests.Commands.Add;

public class AddTaskRequestTests
{
    [Test]
    public void Accepts_project_name_with_task_description_and_string_id()
    {
        var request = new AddTaskRequest("main MA-01 Read a book");
        
        Assert.That(request.ProjectName, Is.EqualTo("main"));
        Assert.That(request.Id.ToString(), Is.EqualTo("MA-01"));
        Assert.That(request.Description, Is.EqualTo("Read a book"));
    }
}