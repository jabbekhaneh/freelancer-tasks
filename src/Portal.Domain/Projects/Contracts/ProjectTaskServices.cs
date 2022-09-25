using Portal.Domain.Projects.DTOs;

namespace Portal.Domain.Projects.Contracts;

public interface ProjectTaskServices
{
    Task<int> Add(AddProjectTaskDto task);
    Task<GetProjectTaskDto> GetById(int projectTaskId);
    Task Update(int projectTaskId,EditProjectTaskDto editProjectTask);
    Task<GetAllProjectTasksByProjectDto> GetAllByProjectId(int projectId);
    Task<(bool IsSucces, string Message)> Remove(int id);
}
