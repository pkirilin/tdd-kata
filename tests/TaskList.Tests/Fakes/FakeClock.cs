namespace TaskList.Tests.Fakes;

public class FakeClock : IClock
{
    public DateOnly CurrentDateUtc => DateOnly.Parse("2023-03-20");
}