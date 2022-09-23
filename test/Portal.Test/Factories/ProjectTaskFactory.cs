using Portal.Domain.Projects;
using Portal.Domain.Projects.DTOs;
using Portal.EF;
using System;
using System.Collections.Generic;

namespace Portal.Test.Factories;

public static class ProjectTaskFactory
{
    public static AddProjectTaskDto GenerateAddProjectTaskDto(int projectId)
    {
        return new AddProjectTaskDto
        {
            Title = "Dummt-Title",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            ProjectId = projectId,
        };
    }

    internal static ProjectTask GenerateProjectTask(EFdbApplication context, int projectId)
    {
        var newProjectTask = new ProjectTask
        {
            Title = "Dummy-Title",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(5),
            ProjectId = projectId,
        };
        context.ProjectTasks.Add(newProjectTask);
        return newProjectTask;
    }
    public static List<ProjectTask> GenerateProjectTasks(EFdbApplication context, int projectId)
    {
        var newProjectTaskS = new List<ProjectTask>
        {
            new ProjectTask
           {
            Title = "Dummy-Title-1",
            StartDate = DateTime.Now.AddHours(1),
            EndDate = DateTime.Now.AddDays(1),
            ProjectId = projectId,
           },
            new ProjectTask
           {
            Title = "Dummy-Title-2",
            StartDate = DateTime.Now.AddHours(5),
            EndDate = DateTime.Now.AddDays(2),
            ProjectId = projectId,
           },

        };
        context.ProjectTasks.AddRange(newProjectTaskS);
        return newProjectTaskS;
    }

    public static EditProjectTaskDto GenerateEditProjectTaskDto(int projectId)
    {
        return new EditProjectTaskDto
        {
            Title = "Dummt-Edit-Title",
            StartDate = DateTime.Now.AddHours(5),
            EndDate = DateTime.Now.AddDays(2),
            ProjectId = projectId,
        };
    }
}
