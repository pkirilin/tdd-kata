namespace TaskList;

public interface IClock
{
    DateOnly CurrentDateUtc { get; }
}