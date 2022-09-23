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

namespace Portal.Test.Portal.ApplicationServices.Projects
{
    public class ProjectTaskApplicationServicesTest
    {
        private EFInMemoryDatabase _memoryContext;
        private EFdbApplication _context;
        private UnitOfWork _unitOfWork;
        private ProjectTaskServices _services;
        private ProjectTaskRepository _projectTaskRepository;
        public ProjectTaskApplicationServicesTest()
        {
            _memoryContext = new EFInMemoryDatabase();
            _context = _memoryContext.CreateDataContext<EFdbApplication>();
            _unitOfWork = new EFUnitOfWork(_context);
            _projectTaskRepository = new EFProjectTaskRepository(_context);
            _services =
                new ProjectTaskApplicationServices
                (_projectTaskRepository, _unitOfWork);
        }

        [Fact]
        private async Task Add_project_task()
        {
            var user = UserFactory.GenerateUser(_context);
            _context.SaveChanges();
            var project = ProjectFactory.GenrateProject(_context, user.Id);
            _context.SaveChanges();
            var addProjectTaskDto = ProjectTaskFactory
                .GenerateAddProjectTaskDto(project.Id);

            var projectTaskId = await _services.Add(addProjectTaskDto);

            var projectTask = _context.ProjetcTasks
                .Single(_ => _.Id == projectTaskId);
            projectTask.Title.Should().Be(addProjectTaskDto.Title);
            projectTask.StartDate.Should().Be(addProjectTaskDto.StartDate);
            projectTask.EndDate.Should().Be(addProjectTaskDto.EndDate);
            projectTask.ProjectId.Should().Be(addProjectTaskDto.ProjectId);
        }
    }
}
