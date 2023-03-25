using TaskList.Contracts;
using TaskList.DataAccess;
using TaskList.Mapping;

namespace TaskList.Features.GetTasksDueToday;

public class GetTasksDueTodayQueryHandler : IHandler<GetTasksDueTodayQuery, GetTasksDueTodayResult>
{
    private readonly IProjectsRepository _projectsRepository;

    public GetTasksDueTodayQueryHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public GetTasksDueTodayResult Handle(GetTasksDueTodayQuery query)
    {
        var projectResponses = new List<ProjectResponse>();
        
        var projects = _projectsRepository.GetAll();
        
        foreach (var project in projects)
        {
            var tasksDueToday = project.GetTasksDueToday();
        
            if (!tasksDueToday.Any())
            {
                continue;
            }

            var taskResponses = tasksDueToday
                .Select(t => t.ToTaskResponse())
                .ToList()
                .AsReadOnly();

            var projectResponse = project.ToProjectResponse(taskResponses);
            projectResponses.Add(projectResponse);
        }

        return new GetTasksDueTodayResult(projectResponses);
    }
}