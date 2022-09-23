using Portal.Domain.Projects;
using Portal.Domain.Projects.DTOs;
using Portal.EF;
using System;

namespace Portal.Test.Factories
{
    public static class ProjectFactory
    {
        public static AddProjectDto GenerateAddProjectDto(string title = "Dummy-title",
                                                          int price = 50)

        {
            return new AddProjectDto
            {
                Title = title,
                PriceTask = price,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                
            };
        }

        public static Project GenrateProject(EFdbApplication context, int userId,bool IsEnd=false)
        {
            var project = new Project
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Title = "Dummy-title",
                PriceTask = 0,
                UserId = userId,
                IsEnd = IsEnd,
            };
            context.Projects.Add(project);
            return project;
        }

        internal static EditProjectDto GenerateEditProjectDto(string title = "Dummy-edit-title",
                                                          int price = 50 + 10)
        {
            return new EditProjectDto
            {
                Title = title,
                PriceTask = price,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            };
        }
    }
}
