using Microsoft.Extensions.DependencyInjection;
using TaskList.DependencyInjection;
using TaskList.Tests.Fakes;
using TaskList.Tests.Fakes.Console;

namespace TaskList.Tests.Dsl.Builders;

public class ApplicationBuilder
{
    public FakeConsole? Console { get; private set; }

    public Application Please()
    {
        var services = new ServiceCollection();
        services.RegisterDependencies();
        ReplaceRealServicesWithFakes(services);

        var serviceProvider = services.BuildServiceProvider();
        Console = serviceProvider.GetRequiredService<IConsole>() as FakeConsole;
        var app = serviceProvider.GetRequiredService<Application>();
        
        return app;
    }

    private static void ReplaceRealServicesWithFakes(IServiceCollection services)
    {
        var realClock = services.First(sd => sd.ServiceType == typeof(IClock));
        services.Remove(realClock);
        services.AddSingleton<IClock, FakeClock>();
        
        var realConsole = services.First(sd => sd.ServiceType == typeof(IConsole));
        services.Remove(realConsole);
        services.AddSingleton<IConsole, FakeConsole>();
    }
}