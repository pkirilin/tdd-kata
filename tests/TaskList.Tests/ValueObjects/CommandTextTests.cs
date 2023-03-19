using TaskList.ValueObjects;

namespace TaskList.Tests.ValueObjects;

public class CommandTextTests
{
    [Test]
    public void Parses_command_with_single_argument()
    {
        var args = new CommandText("command arg");
        
        Assert.That(args.Type, Is.EqualTo("command"));
        Assert.That(args.Arguments, Is.EqualTo(new[] { "arg" }));
        Assert.That(args.ArgumentsText, Is.EqualTo("arg"));
    }
    
    [Test]
    public void Parses_command_with_multiple_arguments()
    {
        var args = new CommandText("command arg1 arg2 another arg", 3);
        
        Assert.That(args.Type, Is.EqualTo("command"));
        Assert.That(args.Arguments, Is.EqualTo(new[] { "arg1", "arg2", "another arg" }));
        Assert.That(args.ArgumentsText, Is.EqualTo("arg1 arg2 another arg"));
    }
}