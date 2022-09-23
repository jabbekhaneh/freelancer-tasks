using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;

namespace Portal.EF.Projects;

public class EFProjectTaskRepository : ProjectTaskRepository
{
    private readonly EFdbApplication _context;

    public EFProjectTaskRepository(EFdbApplication context)
    {
        _context = context;
    }

    public async Task Add(ProjectTask newProjectTask)
    {
        await _context.ProjetcTasks.AddAsync(newProjectTask);
    }
}
