namespace Portal.Domain.Projects.DTOs
{
    public class GetProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnd { get; set; }
        public string? Image { get; set; }
        public decimal PriceTask { get; set; }
    }
}
