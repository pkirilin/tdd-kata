namespace TaskList;

public class Task
{
    public long Id { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public bool Done { get; set; }
}