namespace Portal.Domain.Projects.Contracts
{
    public interface ProjectTaskRepository
    {
        Task Add(ProjectTask newProjectTask);
    }
}
