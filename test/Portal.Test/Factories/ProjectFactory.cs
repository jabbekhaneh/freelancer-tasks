using Portal.Domain.Projects.DTOs;
using System;

namespace Portal.Test.Factories
{
    public static  class ProjectFactory
    {
        public static AddProjectDto GenerateAddProjectDto(string title="Dummy-title",
                                                          int price= 50)
                                                          
        {
            return new AddProjectDto
            {
                Title = title,
                PriceTask=price,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsEnd=false,
            };
        }
    }
}
