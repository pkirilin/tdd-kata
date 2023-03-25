using TaskList.ValueObjects;

namespace TaskList.Tests.ValueObjects;

public class CommandTests
{
    [Test]
    public void Splits_command_text_by_type_and_arguments()
    {
        var command = new Command("command arg1 arg2 another arg");
        
        Assert.That(command.Type, Is.EqualTo("command"));
        Assert.That(command.ArgumentsText, Is.EqualTo("arg1 arg2 another arg"));
    }

    [Test]
    public void Handles_empty_arguments_list()
    {
        var command = new Command("command");
        
        Assert.That(command.Type, Is.EqualTo("command"));
        Assert.That(command.ArgumentsText, Is.Null);
    }
}