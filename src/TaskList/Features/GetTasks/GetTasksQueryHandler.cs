using TaskList.DataAccess;

namespace TaskList.Features.GetTasks;

public class GetTasksQueryHandler : IHandler<GetTasksQuery, GetTasksQueryResult>
{
    private readonly IProjectsRepository _projectsRepository;

    public GetTasksQueryHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }
    
    public GetTasksQueryResult Handle(GetTasksQuery request)
    {
        var tasksQuery = _projectsRepository
            .GetAll()
            .SelectMany(p => p.Tasks);

        if (request.IncludeTasksOnlyWithDueDate)
        {
            tasksQuery = tasksQuery.Where(t => t.DueOn.HasValue);
        }
        
        var tasks = tasksQuery
            .ToList()
            .AsReadOnly();

        return new GetTasksQueryResult(tasks);
    }
}