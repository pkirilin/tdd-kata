namespace TaskList;

public class Clock : IClock
{
    public DateOnly CurrentDateUtc => DateOnly.FromDateTime(DateTime.UtcNow);
}