namespace TaskList.Entities;

public class Project
{
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}