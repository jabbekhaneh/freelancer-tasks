using Portal.Domain;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

namespace Portal.ApplicationServices.Projects;

public class ProjectApplicationServices : ProjectServices
{
    private readonly ProjectRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    public ProjectApplicationServices(ProjectRepository repository,
                                      UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
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
}
