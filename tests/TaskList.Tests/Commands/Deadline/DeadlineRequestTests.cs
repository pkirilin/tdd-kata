using TaskList.Commands.Deadline;

namespace TaskList.Tests.Commands.Deadline;

public class DeadlineRequestTests
{
    [Test]
    public void Can_be_created_from_valid_arguments()
    {
        var request = new DeadlineRequest("1 2023-03-20");
        
        Assert.That(request.TaskId, Is.EqualTo("1"));
        Assert.That(request.Date.ToString("o"), Is.EqualTo("2023-03-20"));
    }
    
    [Test]
    public void Cannot_be_created_with_invalid_deadline_date()
    {
        Assert.Throws<FormatException>(() =>
        {
            var _ = new DeadlineRequest("1 sfdfdsfs");
        });
    }
    
    [Test]
    public void Accepts_string_ids()
    {
        var request = new DeadlineRequest("TASK01 2023-03-20");
        
        Assert.That(request.TaskId, Is.EqualTo("TASK01"));
    }
}