namespace TaskList.Commands;

public abstract class CommandBase
{
    protected CommandBase(string commandLineArgs)
    {
    }
    
    public abstract void Execute();
}