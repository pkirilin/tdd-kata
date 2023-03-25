using TaskList.DataAccess;

namespace TaskList.Features.GetTasks;

public class GetTasksQueryHandler : IHandler<GetTasksQuery, GetTasksQueryResult>
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IClock _clock;

    public GetTasksQueryHandler(IProjectsRepository projectsRepository, IClock clock)
    {
        _projectsRepository = projectsRepository;
        _clock = clock;
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

        if (request.IncludeTasksOnlyDueToday)
        {
            tasksQuery = tasksQuery.Where(t => t.DueOn == _clock.CurrentDateUtc);
        }
        
        var tasks = tasksQuery
            .ToList()
            .AsReadOnly();

        return new GetTasksQueryResult(tasks);
    }
}