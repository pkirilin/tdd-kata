using Microsoft.Extensions.DependencyInjection;
using TaskList.DependencyInjection;

namespace TaskList;

public static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();
        services.AddDependencies();

        var app = services
            .BuildServiceProvider()
            .GetRequiredService<Application>();
        
        app.Run();
    }
}