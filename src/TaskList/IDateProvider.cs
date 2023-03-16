namespace TaskList;

public interface IDateProvider
{
    DateOnly GetCurrentDateUtc();
}