using Microsoft.EntityFrameworkCore;
using Portal.Domain.Projects;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;
using Mapster;
using Portal.ApplicationServices.Common;

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
        await _context.Projects.AddAsync(project);
    }

    public async Task<Project> FindById(int projectId)
    {
        return await _context.Projects.SingleOrDefaultAsync(_ => _.Id == projectId);
    }

    public async Task<GetAllProjectsDto> GetByAll(int userId, int pageId, int take, string? search)
    {
        var result = new GetAllProjectsDto();
        IQueryable<Project> projects = _context.Projects
            .Where(_ => _.UserId == userId);
        if (!string.IsNullOrEmpty(search))
            projects = projects.Where(_ => _.Title.Contains(search));
        var pagination = projects.ProjectToType<GetProjectDto>().Pagination<GetProjectDto>(take, pageId);
        result.Projects = await pagination.Query.OrderBy(_ => _.StartDate).ToListAsync();
        result.PageId = pageId;
        result.PageSize = pagination.PageSize;
        return result;
    }

    public async Task<GetProjectDto> GetById(int projectId)
    {
        return await _context.Projects.ProjectToType<GetProjectDto>()
            .SingleOrDefaultAsync(_ => _.Id == projectId);
    }
}
