using TaskList.Services;

namespace TaskList;

public static class Program
{
    public static void Main(string[] args)
    {
        var clock = new Clock();
        var projectsService = new ProjectsService();
        var application = new Application(new RealConsole(), clock, projectsService);
        
        application.Run();
    }
}