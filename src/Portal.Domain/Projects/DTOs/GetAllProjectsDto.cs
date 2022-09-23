namespace Portal.Domain.Projects.DTOs
{
    public class GetAllProjectsDto
    {
        public GetAllProjectsDto()
        {
            Projects=new List<GetProjectDto>();
        }
        
        public List<GetProjectDto> Projects { get; set; }
        public int PageId { get; set; }
        public int PageSize { get; set; }
    }
}
