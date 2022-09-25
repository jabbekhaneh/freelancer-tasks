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

    public async Task Update(int projectTaskId, EditProjectTaskDto editProjectTask)
    {
        var projectTask= await _repository.FindById(projectTaskId);
        projectTask.Title = editProjectTask.Title;
        projectTask.StartDate=editProjectTask.StartDate;
        projectTask.EndDate=editProjectTask.EndDate;
        await _unitOfWork.CommitAsync();
    }

    public async Task<GetAllProjectTasksByProjectDto> GetAllByProjectId(int projectId)
    {
        return await _repository.GetAll(projectId);
    }

    public async Task<(bool IsSucces, string Message)> Remove(int id)
    {
        var task=await _repository.FindById(id);
        if (task == null)
            return (false, "Notfound task");
        _repository.Remove(task);
        await _unitOfWork.CommitAsync();
        return (true, $"Success  remove task({task.Title})");
    }
}
