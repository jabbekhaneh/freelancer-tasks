using Microsoft.EntityFrameworkCore;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;
using Mapster;
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

    public async Task<GetProjectDto> GetById(int projectId)
    {
        return await _context.Projects.ProjectToType<GetProjectDto>()
            .SingleOrDefaultAsync(_=>_.Id==projectId);
    }
}
