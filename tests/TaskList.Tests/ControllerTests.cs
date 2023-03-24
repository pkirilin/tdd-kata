using Microsoft.Extensions.DependencyInjection;
using TaskList.Actions;
using TaskList.DependencyInjection;

namespace TaskList.Tests;

public class ControllerTests
{
    private IController _controller = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddDependencies();
        
        _controller = services
            .BuildServiceProvider()
            .GetRequiredService<IController>();
    }

    [Test]
    [TestCase("add", typeof(AddAction))]
    [TestCase("check", typeof(CheckAction))]
    [TestCase("deadline", typeof(DeadlineAction))]
    [TestCase("help", typeof(HelpAction))]
    [TestCase("show", typeof(ShowAction))]
    [TestCase("today", typeof(TodayAction))]
    [TestCase("uncheck", typeof(UncheckAction))]
    public void Determines_correct_action_by_command_type(string commandType, Type expectedActionType)
    {
        var action = _controller.GetAction(commandType);
        
        Assert.That(action?.GetType(), Is.EqualTo(expectedActionType));
    }
}