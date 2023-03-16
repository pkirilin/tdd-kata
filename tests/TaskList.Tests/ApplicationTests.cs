namespace TaskList.Tests;

[TestFixture]
public class ApplicationTests
{
    private const string Prompt = "> ";

    private FakeConsole? _console;
    private FakeDateProvider? _dateProvider;
    private Thread? _applicationThread;

    [SetUp]
    public void StartTheApplication()
    {
        _console = new FakeConsole();
        _dateProvider = new FakeDateProvider();
        var taskList = new Application(_console, _dateProvider);
        _applicationThread = new Thread(() => taskList.Run());
        _applicationThread.Start();
    }

    [TearDown]
    public void KillTheApplication()
    {
        if (_applicationThread == null || !_applicationThread.IsAlive)
        {
            return;
        }
        
        _applicationThread.Abort();
        throw new Exception("The application is still running.");
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
            "    [ ] 2: Buy food",
            "",
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