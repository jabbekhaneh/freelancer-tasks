using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;

namespace Portal.EF.Projects;

public class EFProjectRepository : ProjectRepository
{
    private readonly EFdbApplication _context;

    public EFProjectRepository(EFdbApplication context)
    {
        _context = context;
    }

    public async Task Add(Project project)
    {
        await   _context.Projects.AddAsync(project);
    }
}
