using FluentAssertions;
using Portal.ApplicationServices.Projects;
using Portal.ApplicationServices.Projects.Exceptions;
using Portal.Domain;
using Portal.Domain.Projects.Contracts;
using Portal.EF;
using Portal.EF.Projects;
using Portal.Test.Factories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Portal.Test.Portal.ApplicationServices.Projects;

public class ProjectApplicationServicesTest
{
    private EFInMemoryDatabase _memoryContext;
    private EFdbApplication _context;
    private ProjectServices _service;
    private ProjectRepository _projectRepository;
    private UnitOfWork _unitOfWork;
    public ProjectApplicationServicesTest()
    {
        _memoryContext = new EFInMemoryDatabase();
        _context = _memoryContext.CreateDataContext<EFdbApplication>();
        _projectRepository = new EFProjectRepository(_context);
        _unitOfWork = new EFUnitOfWork(_context);
        _service = new ProjectApplicationServices(_projectRepository, _unitOfWork);
    }

    [Fact]
    private async Task Add_Project()
    {
        var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        int userId = user.Id;
        var AddProjectDto = ProjectFactory.GenerateAddProjectDto();

        var projectIdActual = await _service.Add(AddProjectDto, userId, "");

        var project = _context.Projects.Single(_ => _.Id == projectIdActual);
        project.Title.Should().Be(AddProjectDto.Title);
        project.StartDate.Should().Be(AddProjectDto.StartDate);
        project.EndDate.Should().Be(AddProjectDto.EndDate);
        project.PriceTask.Should().Be(AddProjectDto.PriceTask);
        project.UserId.Should().Be(user.Id);

    }

    [Fact]
    private async Task Get_project_by_id()
    {
        var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        var project = ProjectFactory.GenrateProject(_context, user.Id);
        _context.SaveChanges();

        var getProject = await _service.GetById(project.Id);

        getProject.Title.Should().Be(project.Title);
        getProject.StartDate.Should().Be(project.StartDate);
        getProject.EndDate.Should().Be(project.EndDate);
        getProject.PriceTask.Should().Be(project.PriceTask);
        getProject.IsEnd.Should().Be(project.IsEnd);

    }

    [Fact]
    private async Task Update_project()
    {
        var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        var project = ProjectFactory.GenrateProject(_context, user.Id);
        _context.SaveChanges();
        var editProjectDto = ProjectFactory.GenerateEditProjectDto();

        await _service.Update(project.Id, editProjectDto);

        var getProject = _context.Projects.First(_ => _.Id == project.Id);
        getProject.Title.Should().Be(editProjectDto.Title);
        getProject.StartDate.Should().Be(editProjectDto.StartDate);
        getProject.EndDate.Should().Be(editProjectDto.EndDate);
        getProject.PriceTask.Should().Be(editProjectDto.PriceTask);
    }

    [Fact]
    private async Task Update_project_exception_when_project_isEnd()
    {
        var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        var project = ProjectFactory.GenrateProject(_context, user.Id,true);
        _context.SaveChanges();
        var editProjectDto = ProjectFactory.GenerateEditProjectDto();

        Func<Task> exepted = () => _service.Update(project.Id, editProjectDto);

        exepted.Should().ThrowAsync<ProjectIsEndException>();
    }

    [Fact]
    private async Task Get_all()
    {
        var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        ProjectFactory.GenrateProjects(_context, user.Id);
        _context.SaveChanges();

        var projects = await _service.GetAll(user.Id, 1, 10, "");

        projects.Projects.Count().Should().Be(3);
    }
}
