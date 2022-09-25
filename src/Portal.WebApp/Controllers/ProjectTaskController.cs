using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Projects.Contracts;

namespace Portal.WebApp.Controllers;

[Authorize]
public class ProjectTaskController : Controller
{
    private readonly ProjectTaskServices _projectTaskServices;

    public ProjectTaskController(ProjectTaskServices projectTaskServices)
    {
        _projectTaskServices = projectTaskServices;
    }

    public async Task<IActionResult> Index(int id)
    {
        var projectTasks= await _projectTaskServices.GetAllByProjectId(id);
        return View(projectTasks);
    }
}
