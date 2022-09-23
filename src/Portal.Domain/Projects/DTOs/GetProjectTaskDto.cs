namespace Portal.Domain.Projects.DTOs;

public class GetProjectTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProjectId { get; set; }
}
