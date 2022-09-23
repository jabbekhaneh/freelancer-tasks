using Portal.Domain.Projects;
using Portal.Domain.Projects.DTOs;
using Portal.EF;
using System;
using System.Collections.Generic;

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

        public static Project GenrateProject(EFdbApplication context, int userId, bool IsEnd = false)
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
        public static List<Project> GenrateProjects(EFdbApplication context, int userId, bool IsEnd = false)
        {
            var projects = new List<Project>
            {
                 new Project
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Title = "Dummy-title-1",
                PriceTask = 20,
                UserId = userId,
                IsEnd = IsEnd,
            },
                  new Project
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
                Title = "Dummy-title-2",
                PriceTask = 15,
                UserId = userId,
                IsEnd = IsEnd,
            },
                   new Project
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(3),
                Title = "Dummy-title-3",
                PriceTask = 50,
                UserId = userId,
                IsEnd = IsEnd,
            }
        };
            context.Projects.AddRangeAsync(projects);
            return projects;
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
