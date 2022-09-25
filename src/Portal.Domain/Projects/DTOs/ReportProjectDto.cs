namespace Portal.Domain.Projects.DTOs;

public class ReportProjectDto
{
    public string Title { get; set; }
    public decimal PriceTaskHours { get; set; }
    public decimal TotalFactor { get; set; }
    public int TotalHours { get; set; }
}
