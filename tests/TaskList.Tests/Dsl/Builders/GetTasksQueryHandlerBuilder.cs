using Moq;
using TaskList.DataAccess;
using TaskList.Entities;
using TaskList.Features;
using TaskList.Features.GetTasks;
using TaskList.Tests.Fakes;

namespace TaskList.Tests.Dsl.Builders;

public class GetTasksQueryHandlerBuilder
{
    private static readonly IClock Clock = new FakeClock();
    private readonly Mock<IProjectsRepository> _projectsRepositoryMock = new();

    public IHandler<GetTasksQuery, GetTasksQueryResult> Please()
    {
        return new GetTasksQueryHandler(_projectsRepositoryMock.Object, Clock);
    }

    public GetTasksQueryHandlerBuilder WithProjects(params Project[] projects)
    {
        _projectsRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(projects);
        return this;
    }
}