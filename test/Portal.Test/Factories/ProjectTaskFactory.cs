using Portal.Domain.Projects;
using Portal.Domain.Projects.DTOs;
using Portal.EF;
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

    internal static ProjectTask GenerateProjectTask(EFdbApplication context,int projectId)
    {
        var newProjectTask = new ProjectTask
        {
            Title = "Dummy-Title",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            ProjectId =projectId,
        };
        context.ProjectTasks.Add(newProjectTask);
        return newProjectTask;
    }
}
