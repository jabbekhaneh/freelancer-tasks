using Portal.Domain.Projects.DTOs;

namespace Portal.Domain.Projects.Contracts;

public interface ProjectRepository
{
    Task Add(Project project);
    Task<GetProjectDto> GetById(int projectId);
    Task<Project> FindById(int projectId);
}
