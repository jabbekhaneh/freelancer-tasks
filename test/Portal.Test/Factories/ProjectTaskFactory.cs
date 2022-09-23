using Portal.Domain.Projects.DTOs;
using System;

namespace Portal.Test.Factories;

public static class ProjectTaskFactory
{
    public static AddProjectTaskDto GenerateAddProjectTaskDto(int projectId)
    {
        return new AddProjectTaskDto
        {
            Title="Dummt-Title",
            StartDate = DateTime.Now,
            EndDate= DateTime.Now.AddDays(1),
            ProjectId = projectId,
        };
    }
}
