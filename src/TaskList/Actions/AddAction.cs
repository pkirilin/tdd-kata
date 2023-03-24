using TaskList.Features;
using TaskList.Features.AddProject;
using TaskList.Features.AddTask;
using TaskList.ValueObjects;

namespace TaskList.Actions;

public class AddAction : IAction
{
    private readonly IHandler<AddProjectCommand> _addProjectHandler;
    private readonly IHandler<AddTaskCommand> _addTaskHandler;

    public AddAction(IHandler<AddProjectCommand> addProjectHandler, IHandler<AddTaskCommand> addTaskHandler)
    {
        _addProjectHandler = addProjectHandler;
        _addTaskHandler = addTaskHandler;
    }
    
    public string CommandType => "add";
    
    public void Execute(string? argumentsInputText)
    {
        var subcommand = new Command(argumentsInputText);

        switch (subcommand.Type)
        {
            case "project":
                _addProjectHandler.Handle(new AddProjectCommand(subcommand.ArgumentsText));
                break;
            case "task":
                _addTaskHandler.Handle(new AddTaskCommand(subcommand.ArgumentsText));
                break;
        }
    }
}