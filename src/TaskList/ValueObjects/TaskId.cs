namespace TaskList.ValueObjects;

public class TaskId
{
    public string Value { get; }
    
    public TaskId(string input)
    {
        Value = input;
    }

    public override string ToString()
    {
        return Value;
    }

    public static bool operator==(TaskId first, TaskId second)
    {
        return first.Value == second.Value;
    }

    public static bool operator!=(TaskId first, TaskId second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskId)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    private bool Equals(TaskId other)
    {
        return Value == other.Value;
    }
}