using Moq;
using TaskList.Commands;
using TaskList.Commands.Today;
using TaskList.Entities;
using TaskList.Services;

namespace TaskList.Tests.Dsl.Builders;

public class TodayCommandBuilder
{
    private readonly Mock<IProjectsService> _projectsServiceMock = new();
    private readonly Mock<IConsole> _consoleMock = new();

    public Mock<IConsole> ConsoleMock => _consoleMock;

    public TodayCommand Please()
    {
        return new TodayCommand(_projectsServiceMock.Object, _consoleMock.Object);
    }

    public TodayCommandBuilder WithProjects(params Project[] projects)
    {
        _projectsServiceMock
            .Setup(x => x.GetAll())
            .Returns(projects);
        return this;
    }
}