using TaskList.Tests.Dsl.Builders;

namespace TaskList.Tests.Dsl;

public static class Create
{
    public static ApplicationBuilder Application() => new();
    
    public static TaskBuilder Task() => new();
    public static ProjectBuilder Project() => new();

    public static GetTasksQueryHandlerBuilder GetTasksQueryHandler() => new();
    public static SetDeadlineQueryHandlerBuilder SetDeadlineQueryHandler() => new();
    public static ShowTasksDueTodayQueryHandlerBuilder ShowTasksDueTodayQueryHandler() => new();
}