using TaskList.ValueObjects;

namespace TaskList.Tests.ValueObjects;

public class TaskIdTests
{
    [Test]
    public void Can_be_created_from_user_input()
    {
        const string input = "Byc9F";
        
        var id = new TaskId(input);
        
        Assert.That(id.Value, Is.EqualTo(input));
        Assert.That(id.ToString(), Is.EqualTo(input));
    }

    [Test]
    public void Checks_equality_by_comparing_values()
    {
        var firstId = new TaskId("Byc9F");
        var secondId = new TaskId("Byc9F");

        var areEqual = firstId == secondId;
        
        Assert.That(areEqual, Is.True);
    }
}