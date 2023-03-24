using Moq;
using TaskList.DataAccess;
using TaskList.Entities;
using TaskList.Features.ShowTasksDueToday;

namespace TaskList.Tests.Dsl.Builders;

public class ShowTasksDueTodayQueryHandlerBuilder
{
    private readonly Mock<IProjectsRepository> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public ShowTasksDueTodayQueryHandler Please()
    {
        return new ShowTasksDueTodayQueryHandler(_projectsServiceMock.Object, _consoleMock.Object);
    }

    public ShowTasksDueTodayQueryHandlerBuilder WithProjects(params Project[] projects)
    {
        _projectsServiceMock
            .Setup(x => x.GetAll())
            .Returns(projects);
        return this;
    }
}