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

            var projectTask = _context.ProjectTasks
                .Single(_ => _.Id == projectTaskId);
            projectTask.Title.Should().Be(addProjectTaskDto.Title);
            projectTask.StartDate.Should().Be(addProjectTaskDto.StartDate);
            projectTask.EndDate.Should().Be(addProjectTaskDto.EndDate);
            projectTask.ProjectId.Should().Be(addProjectTaskDto.ProjectId);
        }

        [Fact]
        private async Task Get_project_task_by_id()
        {
            var user = UserFactory.GenerateUser(context: _context);
            _context.SaveChanges();
            var project = ProjectFactory.GenrateProject(_context, user.Id);
            _context.SaveChanges();
            var projectTask = ProjectTaskFactory.GenerateProjectTask(_context, project.Id);
            _context.SaveChanges();

            var getProjectTask = await _services.GetById(projectTask.Id);

            getProjectTask.Title.Should().Be(projectTask.Title);
            getProjectTask.StartDate.Should().Be(projectTask.StartDate);
            getProjectTask.EndDate.Should().Be(projectTask.EndDate);
            getProjectTask.ProjectId.Should().Be(projectTask.ProjectId);
        }

        [Fact]
        private async Task Update_project_task()
        {
            var user = UserFactory.GenerateUser(context: _context);
            _context.SaveChanges();
            var project = ProjectFactory.GenrateProject(_context, user.Id);
            _context.SaveChanges();
            var projectTask = ProjectTaskFactory.GenerateProjectTask(_context, project.Id);
            _context.SaveChanges();
            var editProjectTaskDto = ProjectTaskFactory.GenerateEditProjectTaskDto(project.Id);

            await _services.Update(projectTask.Id, editProjectTaskDto);

            var getProjectTask=_context.ProjectTasks
                .FirstOrDefault(_=>_.Id==projectTask.Id);
            getProjectTask.Title.Should().Be(editProjectTaskDto.Title);
            getProjectTask.StartDate.Should().Be(editProjectTaskDto.StartDate);
            getProjectTask.EndDate.Should().Be(editProjectTaskDto.EndDate);
        }
    }
}
