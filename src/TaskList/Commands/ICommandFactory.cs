namespace TaskList.Commands;

public interface ICommandFactory
{
    ICommand CreateCommand();
}