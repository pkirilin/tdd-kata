using System.Text.RegularExpressions;

namespace TaskList.ValueObjects;

public class TaskId
{
    public string Value { get; }
    
    public TaskId(string input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Task ID value is invalid", nameof(input));
        }
        
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
        return obj.GetType() == GetType() && Equals((TaskId)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
    private bool Equals(TaskId other)
    {
        return Value == other.Value;
    }

    private static bool ValidateInput(string input)
    {
        // var regexItem = new Regex("^[a-zA-Z0-9]*$");

        // return regexItem.IsMatch(input);

        return Regex.IsMatch(input, "^[a-zA-Z0-9]*$");
    }
}