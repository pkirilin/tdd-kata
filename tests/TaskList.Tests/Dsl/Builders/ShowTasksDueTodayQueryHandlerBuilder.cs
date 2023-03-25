using Moq;
using TaskList.DataAccess;
using TaskList.Entities;
using TaskList.Features.GetTasksDueToday;

namespace TaskList.Tests.Dsl.Builders;

public class ShowTasksDueTodayQueryHandlerBuilder
{
    private readonly Mock<IProjectsRepository> _projectsRepositoryMock = new();

    public GetTasksDueTodayQueryHandler Please()
    {
        return new GetTasksDueTodayQueryHandler(_projectsRepositoryMock.Object);
    }

    public ShowTasksDueTodayQueryHandlerBuilder WithProjects(params Project[] projects)
    {
        _projectsRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(projects);
        return this;
    }
}