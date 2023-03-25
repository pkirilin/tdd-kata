using TaskList.Tests.Dsl;
using TaskList.Tests.Fakes.Console;

namespace TaskList.Tests;

[TestFixture]
public class ApplicationTests
{
    private const string Prompt = "> ";

    private FakeConsole? _console;
    private CancellationTokenSource _cancellationTokenSource = null!;
    private Task? _applicationTask;

    [SetUp]
    public void StartTheApplication()
    {
        var appBuilder = Create.Application();
        var app = appBuilder.Please();
        _console = appBuilder.Console;
        _cancellationTokenSource = new CancellationTokenSource();
        _applicationTask = Task.Run(() => app.Run(), _cancellationTokenSource.Token);
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
        Execute("view by project");

        Execute("add project secrets");
        Execute("add task secrets SEC001 Eat more donuts.");
        Execute("add task secrets SEC002 Destroy all humans.");

        Execute("view by project");
        ReadLines(
            "secrets",
            "    [ ] SEC001: Eat more donuts.",
            "    [ ] SEC002: Destroy all humans.",
            ""
        );

        Execute("add project training");
        Execute("add task training TRA001 Four Elements of Simple Design");
        Execute("add task training TRA002 SOLID");
        Execute("add task training TRA003 Coupling and Cohesion");
        Execute("add task training TRA004 Primitive Obsession");
        Execute("add task training TRA005 Outside-In TDD");
        Execute("add task training TRA006 Interaction-Driven Design");

        Execute("check SEC001");
        Execute("check TRA001");
        Execute("check TRA003");
        Execute("check TRA004");

        Execute("view by project");
        ReadLines(
            "secrets",
            "    [x] SEC001: Eat more donuts.",
            "    [ ] SEC002: Destroy all humans.",
            "",
            "training",
            "    [x] TRA001: Four Elements of Simple Design",
            "    [ ] TRA002: SOLID",
            "    [x] TRA003: Coupling and Cohesion",
            "    [x] TRA004: Primitive Obsession",
            "    [ ] TRA005: Outside-In TDD",
            "    [ ] TRA006: Interaction-Driven Design",
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
        
        Execute("view by project");
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

    [Test, Timeout(1000)]
    public void User_can_delete_task_by_id()
    {
        Execute("add project main");
        Execute("add task main 44bMf Read a book");
        Execute("add task main h4Nq5 Buy food");
        
        Execute("view by project");
        ReadLines(
            "main",
            "    [ ] 44bMf: Read a book",
            "    [ ] h4Nq5: Buy food",
            ""
        );
        
        Execute("delete 44bMf");
        
        Execute("view by project");
        ReadLines(
            "main",
            "    [ ] h4Nq5: Buy food",
            ""
        );
    }
    
    [Test, Timeout(1000)]
    public void User_can_view_tasks_by_deadline()
    {
        Execute("add project main");
        Execute("add task main 44bMf Read a book");
        Execute("add task main 53bTD Go for a walk");
        Execute("add task main h4Nq5 Buy food");
        
        Execute("deadline h4Nq5 2023-03-20");
        Execute("deadline 53bTD 2023-03-21");
        
        Execute("view by deadline");
        ReadLines(
            "2023-03-20",
            "    [ ] h4Nq5: Buy food",
            "",
            "2023-03-21",
            "    [ ] 53bTD: Go for a walk"
        );
    }
    
    [Test, Timeout(1000)]
    public void User_can_view_tasks_by_project()
    {
        Execute("add project first");
        Execute("add project second");
        Execute("add task first 44bMf Read a book");
        Execute("add task first 53bTD Go for a walk");
        Execute("add task second h4Nq5 Buy food");

        Execute("view by project");
        ReadLines(
            "first",
            "    [ ] 44bMf: Read a book",
            "    [ ] 53bTD: Go for a walk",
            "",
            "second",
            "    [ ] h4Nq5: Buy food"
        );
    }

    private void Execute(string command)
    {
        Read(Prompt);
        Write(command);
    }

    private void Read(string expectedOutput)
    {
        var actualOutput = _console?.RetrieveOutput(expectedOutput.Length);
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
        _console?.SendInput(input + Environment.NewLine);
    }
}