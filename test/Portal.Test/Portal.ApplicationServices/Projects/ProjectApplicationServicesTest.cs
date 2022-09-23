using FluentAssertions;
using Portal.ApplicationServices.Projects;
using Portal.Domain;
using Portal.Domain.Projects.Contracts;
using Portal.EF;
using Portal.EF.Projects;
using Portal.Test.Factories;
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
        var user= UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        int userId= user.Id;
        var AddProjectDto = ProjectFactory.GenerateAddProjectDto();
        
        var projectIdActual= await _service.Add(AddProjectDto, userId, "");

        var project = _context.Projects.Single(_ => _.Id == projectIdActual);
        project.Title.Should().Be(AddProjectDto.Title);
        project.StartDate.Should().Be(AddProjectDto.StartDate);
        project.EndDate.Should().Be(AddProjectDto.EndDate);
        project.PriceTask.Should().Be(AddProjectDto.PriceTask);
        project.UserId.Should().Be(user.Id);

    }
}
