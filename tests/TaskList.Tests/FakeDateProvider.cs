namespace TaskList.Tests;

public class FakeDateProvider : IDateProvider
{
    public DateOnly GetCurrentDateUtc()
    {
        return DateOnly.Parse("2023-03-20");
    }
}