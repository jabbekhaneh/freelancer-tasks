namespace Portal.Domain.Projects.DTOs;

public class GetAllProjectTasksByProjectDto
{
    public GetAllProjectTasksByProjectDto()
    {
        ProjectTasks = new List<GetProjectTaskDto>();
    }
    public List<GetProjectTaskDto> ProjectTasks { get; set; }
    public string Title { get; set; }
    public int ProjectId { get; set; }

}
