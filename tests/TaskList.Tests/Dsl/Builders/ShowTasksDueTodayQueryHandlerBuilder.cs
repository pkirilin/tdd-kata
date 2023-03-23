using Moq;
using TaskList.Entities;
using TaskList.Features.ShowTasksDueToday;
using TaskList.Services;

namespace TaskList.Tests.Dsl.Builders;

public class ShowTasksDueTodayQueryHandlerBuilder
{
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
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