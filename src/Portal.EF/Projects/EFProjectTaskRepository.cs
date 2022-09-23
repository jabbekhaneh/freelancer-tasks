using Mapster;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

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
        await _context.ProjectTasks.AddAsync(newProjectTask);
    }

    public async Task<GetProjectTaskDto> GetById(int projectTaskId)
    {
        return await _context.ProjectTasks
            .Where(_ => _.Id == projectTaskId)
            .ProjectToType<GetProjectTaskDto>()
            .FirstOrDefaultAsync();
    }
}
