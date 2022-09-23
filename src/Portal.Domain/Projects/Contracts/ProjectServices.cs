﻿using Portal.Domain.Projects.DTOs;

namespace Portal.Domain.Projects.Contracts
{
    public interface ProjectServices
    {
        Task<int> Add(AddProjectDto project, int userId,string? image);
        Task<GetProjectDto> GetById(int projectId);
        Task Update(int projectId,EditProjectDto editProjectDto);
    }
}
