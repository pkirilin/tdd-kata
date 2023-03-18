using TaskList.Services;
using TaskList.Tests.Fakes;
using TaskList.Tests.Fakes.Console;

namespace TaskList.Tests;

[TestFixture]
public class ApplicationTests
{
    private const string Prompt = "> ";

    private FakeConsole _console = null!;
    private FakeClock _clock = null!;
    private CancellationTokenSource _cancellationTokenSource = null!;
    private Task? _applicationTask;

    [SetUp]
    public void StartTheApplication()
    {
        _console = new FakeConsole();
        _clock = new FakeClock();
        var projectsService = new ProjectsService();
        var application = new Application(_console, _clock, projectsService);
        _cancellationTokenSource = new CancellationTokenSource();
        _applicationTask = Task.Run(() => application.Run(), _cancellationTokenSource.Token);
    }

    [TearDown]
    public void KillTheApplication()
    {
        if (_applicationTask is { IsCompleted: false })
        {
            _cancellationTokenSource.Cancel();
        }
    }

    [Test, Timeout(1000)]
    public void ItWorks()
    {
        Execute("show");

        Execute("add project secrets");
        Execute("add task secrets Eat more donuts.");
        Execute("add task secrets Destroy all humans.");

        Execute("show");
        ReadLines(
            "secrets",
            "    [ ] 1: Eat more donuts.",
            "    [ ] 2: Destroy all humans.",
            ""
        );

        Execute("add project training");
        Execute("add task training Four Elements of Simple Design");
        Execute("add task training SOLID");
        Execute("add task training Coupling and Cohesion");
        Execute("add task training Primitive Obsession");
        Execute("add task training Outside-In TDD");
        Execute("add task training Interaction-Driven Design");

        Execute("check 1");
        Execute("check 3");
        Execute("check 5");
        Execute("check 6");

        Execute("show");
        ReadLines(
            "secrets",
            "    [x] 1: Eat more donuts.",
            "    [ ] 2: Destroy all humans.",
            "",
            "training",
            "    [x] 3: Four Elements of Simple Design",
            "    [ ] 4: SOLID",
            "    [x] 5: Coupling and Cohesion",
            "    [x] 6: Primitive Obsession",
            "    [ ] 7: Outside-In TDD",
            "    [ ] 8: Interaction-Driven Design",
            ""
        );

        Execute("quit");
    }

    [Test, Timeout(1000)]
    public void User_can_give_a_task_an_optional_deadline()
    {
        Execute("add project main");
        Execute("add task main Read a book");
        Execute("add task main Buy food");
        
        Execute("show");
        ReadLines(
            "main",
            "    [ ] 1: Read a book",
            "    [ ] 2: Buy food",
            ""
        );
        
        Execute("deadline 1 2023-03-20");
        Execute("today");
        ReadLines(
            "main",
            "    [ ] 1: Read a book",
            ""
        );

        Execute("quit");
    }

    private void Execute(string command)
    {
        Read(Prompt);
        Write(command);
    }

    private void Read(string expectedOutput)
    {
        var actualOutput = _console.RetrieveOutput(expectedOutput.Length);
        Assert.That(actualOutput, Is.EqualTo(expectedOutput));
    }

    private void ReadLines(params string[] expectedOutput)
    {
        foreach (var line in expectedOutput)
        {
            Read(line + Environment.NewLine);
        }
    }

    private void Write(string input)
    {
        _console.SendInput(input + Environment.NewLine);
    }
}