using TaskList.Features.AddTask;

namespace TaskList.Tests.Features.AddTask;

public class AddTaskCommandTests
{
    [Test]
    public void Accepts_project_name_with_task_description_and_string_id()
    {
        var command = new AddTaskCommand("main fdsfsd1 Read a book");
        
        Assert.That(command.ProjectName, Is.EqualTo("main"));
        Assert.That(command.Id, Is.EqualTo("fdsfsd1"));
        Assert.That(command.Description, Is.EqualTo("Read a book"));
    }
}