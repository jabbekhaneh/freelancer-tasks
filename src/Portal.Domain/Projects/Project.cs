using Portal.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Projects;

public class Project
{
    public Project()
    {
        ProjectTasks = new HashSet<ProjectTask>();
    }
    [Required,MaxLength(250)]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public bool IsEnd { get; set; }
    public string? Image { get; set; }
    [Required]
    public decimal PriceTask { get; set; }
    public int UserId { get; set; }
    #region NavigationProperty
    public ICollection<ProjectTask> ProjectTasks  { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    #endregion 
}
