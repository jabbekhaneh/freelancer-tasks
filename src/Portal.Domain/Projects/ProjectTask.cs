using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Domain.Projects;

public class ProjectTask
{
    [Required,MaxLength(250)]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public int ProjectId { get; set; }

    #region NavigationProperty
    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
    #endregion

}
