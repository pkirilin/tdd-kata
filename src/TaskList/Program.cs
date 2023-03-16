namespace TaskList;

public static class Program
{
    public static void Main(string[] args)
    {
        new Application(new RealConsole(), null).Run();
    }
}