using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Projects.DTOs
{
    public class AddProjectTaskDto
    {
        [Required, MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
    }
}
