using TaskList.Tests.Dsl.Builders;

namespace TaskList.Tests.Dsl;

public static class Create
{
    public static TaskBuilder Task() => new();
    public static ProjectBuilder Project() => new();
    
    public static DeadlineCommandBuilder DeadlineCommand() => new();
    public static TodayCommandBuilder TodayCommand() => new();
}