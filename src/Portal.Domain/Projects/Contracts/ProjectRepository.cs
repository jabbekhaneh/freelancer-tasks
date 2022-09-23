namespace Portal.Domain.Projects.Contracts;

public interface ProjectRepository
{
    Task Add(Project project);
}
