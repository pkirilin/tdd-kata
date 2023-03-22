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
    public void User_can_add_view_and_check_tasks()
    {
        Execute("show");

        Execute("add project secrets");
        Execute("add task secrets SEC-001 Eat more donuts.");
        Execute("add task secrets SEC-002 Destroy all humans.");

        Execute("show");
        ReadLines(
            "secrets",
            "    [ ] SEC-001: Eat more donuts.",
            "    [ ] SEC-002: Destroy all humans.",
            ""
        );

        Execute("add project training");
        Execute("add task training TRA-001 Four Elements of Simple Design");
        Execute("add task training TRA-002 SOLID");
        Execute("add task training TRA-003 Coupling and Cohesion");
        Execute("add task training TRA-004 Primitive Obsession");
        Execute("add task training TRA-005 Outside-In TDD");
        Execute("add task training TRA-006 Interaction-Driven Design");

        Execute("check SEC-001");
        Execute("check TRA-001");
        Execute("check TRA-003");
        Execute("check TRA-004");

        Execute("show");
        ReadLines(
            "secrets",
            "    [x] SEC-001: Eat more donuts.",
            "    [ ] SEC-002: Destroy all humans.",
            "",
            "training",
            "    [x] TRA-001: Four Elements of Simple Design",
            "    [ ] TRA-002: SOLID",
            "    [x] TRA-003: Coupling and Cohesion",
            "    [x] TRA-004: Primitive Obsession",
            "    [ ] TRA-005: Outside-In TDD",
            "    [ ] TRA-006: Interaction-Driven Design",
            ""
        );

        Execute("quit");
    }

    [Test, Timeout(1000)]
    public void User_can_give_a_task_an_optional_deadline()
    {
        Execute("add project main");
        Execute("add task main 44bMf Read a book");
        Execute("add task main h4Nq5 Buy food");
        
        Execute("show");
        ReadLines(
            "main",
            "    [ ] 44bMf: Read a book",
            "    [ ] h4Nq5: Buy food",
            ""
        );
        
        Execute("deadline 44bMf 2023-03-20");
        Execute("today");
        ReadLines(
            "main",
            "    [ ] 44bMf: Read a book",
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