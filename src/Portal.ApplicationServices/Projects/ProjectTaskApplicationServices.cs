using Portal.Domain;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

namespace Portal.ApplicationServices.Projects;

public class ProjectTaskApplicationServices : ProjectTaskServices
{
    private readonly ProjectTaskRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    public ProjectTaskApplicationServices(ProjectTaskRepository repository,
                                          UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Add(AddProjectTaskDto task)
    {
        ProjectTask newProjectTask = CreateProjectTask(task);
        await _repository.Add(newProjectTask);
        await _unitOfWork.CommitAsync();
        return newProjectTask.Id;
    }

    private static ProjectTask CreateProjectTask(AddProjectTaskDto task)
    {
        return new ProjectTask
        {
            Title = task.Title,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            ProjectId = task.ProjectId,
        };
    }

    public async Task<GetProjectTaskDto> GetById(int projectTaskId)
    {
        return await _repository.GetById(projectTaskId);
    }
}
