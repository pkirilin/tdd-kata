using Microsoft.Extensions.DependencyInjection;
using TaskList.Services;

namespace TaskList.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
        services.AddSingleton<IProjectsService, ProjectsService>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IConsole, RealConsole>();
    }
}