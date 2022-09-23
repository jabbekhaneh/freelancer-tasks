using Portal.Domain.Projects.DTOs;

namespace Portal.Domain.Projects.Contracts;

public interface ProjectTaskServices
{
    Task<int> Add(AddProjectTaskDto task);
    Task<GetProjectTaskDto> GetById(int projectTaskId);
}
