using Microsoft.Extensions.DependencyInjection;
using TaskList.Actions;
using TaskList.DataAccess;
using TaskList.Features;
using TaskList.Features.AddProject;
using TaskList.Features.AddTask;
using TaskList.Features.GetTasksDueToday;
using TaskList.Features.SetDeadline;
using TaskList.Features.SetDone;

namespace TaskList.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
        services.AddSingleton<IProjectsRepository, InMemoryProjectsRepository>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IConsole, RealConsole>();
        services.AddSingleton<IController, Controller>();
        services.AddControllerWithActions();
        services.AddHandlers();
    }

    private static void AddControllerWithActions(this IServiceCollection services)
    {
        services.AddSingleton<IAction, AddAction>();
        services.AddSingleton<IAction, CheckAction>();
        services.AddSingleton<IAction, DeadlineAction>();
        services.AddSingleton<IAction, HelpAction>();
        services.AddSingleton<IAction, ShowAction>();
        services.AddSingleton<IAction, TodayAction>();
        services.AddSingleton<IAction, UncheckAction>();
    }

    private static void AddHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IHandler<AddProjectCommand>, AddProjectCommandHandler>();
        services.AddSingleton<IHandler<AddTaskCommand>, AddTaskCommandHandler>();
        services.AddSingleton<IHandler<SetDeadlineCommand>, SetDeadlineCommandHandler>();
        services.AddSingleton<IHandler<SetDoneCommand>, SetDoneCommandHandler>();
        services.AddSingleton<IHandler<GetTasksDueTodayQuery, GetTasksDueTodayResult>, GetTasksDueTodayQueryHandler>();
    }
}