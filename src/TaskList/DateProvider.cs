namespace TaskList;

public class DateProvider : IDateProvider
{
    public DateOnly GetCurrentDateUtc()
    {
        return DateOnly.FromDateTime(DateTime.UtcNow);
    }
}