using Portal.ApplicationServices.Projects.Exceptions;
using Portal.Domain;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

namespace Portal.ApplicationServices.Projects;

public class ProjectApplicationServices : ProjectServices
{
    private readonly ProjectRepository _repository;
    private readonly ProjectTaskRepository _projectTaskRepository;
    private readonly UnitOfWork _unitOfWork;
    public ProjectApplicationServices(ProjectRepository repository,
                                      UnitOfWork unitOfWork,
                                      ProjectTaskRepository projectTaskRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task<int> Add(AddProjectDto project, int userId, string? image)
    {
        Project newProject = CreateNewProject(project, userId, image);
        await _repository.Add(newProject);
        await _unitOfWork.CommitAsync();
        return newProject.Id;
    }

    private static Project CreateNewProject(AddProjectDto project, int userId, string? image)
    {
        return new Project
        {
            IsEnd = false,
            Image = image,
            UserId = userId,
            PriceTask = project.PriceTask,
            Title = project.Title,
            EndDate = project.EndDate,
            StartDate = project.StartDate,
        };
    }

    public async Task<GetProjectDto> GetById(int projectId)
    {
        return await _repository.GetById(projectId);
    }

    public async Task Update(int projectId, EditProjectDto editProjectDto)
    {
        var project = await _repository.FindById(projectId);
        if (project.IsEnd || project.EndDate < DateTime.Now)
            throw new ProjectIsEndException();
        if (editProjectDto.Image != null)
            project.Image = editProjectDto.Image;
        project.Title = editProjectDto.Title;
        project.EndDate = editProjectDto.EndDate;
        project.StartDate = editProjectDto.StartDate;
        project.PriceTask = editProjectDto.PriceTask;

        await _unitOfWork.CommitAsync();

    }

    public async Task<GetAllProjectsDto> GetAll(int userId, int pageId, int take, string? search)
    {
        return await _repository.GetByAll(userId, pageId, take, search);
    }

    public async Task<ReportProjectDto> ReportProject(int projectId)
    {
        var report = new ReportProjectDto();
        var project = await _repository.FindById(projectId);
        report.Title = project.Title;
        report.PriceTaskHours = project.PriceTask;
        var tasks = await _projectTaskRepository.GetAll(projectId);
        foreach (var task in tasks.ProjectTasks)
        {
            var minutes = (int)(task.EndDate - task.StartDate).TotalMinutes;
            report.TotalMinute += minutes;
        }
        report.TotalFactor = (report.TotalMinute / 60) * project.PriceTask;
        
        return report;
    }
}
