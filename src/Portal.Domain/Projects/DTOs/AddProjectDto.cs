﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Projects.DTOs
{
    public class AddProjectDto
    {
        [Required, MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool IsEnd { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public decimal PriceTask { get; set; }
    }
}
