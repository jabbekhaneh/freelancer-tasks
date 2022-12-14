using Portal.Domain.Projects.DTOs;

namespace Portal.Domain.Projects.Contracts
{
    public interface ProjectTaskRepository
    {
        Task Add(ProjectTask newProjectTask);
        Task<GetProjectTaskDto> GetById(int projectTaskId);
        Task<ProjectTask> FindById(int projectTaskId);
        Task<GetAllProjectTasksByProjectDto> GetAll(int projectId);
        void Remove(ProjectTask task);
    }
}
