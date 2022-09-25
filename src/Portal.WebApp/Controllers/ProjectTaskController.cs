using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

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
        var projectTasks = await _projectTaskServices.GetAllByProjectId(id);
        return View(projectTasks);
    }
    public IActionResult Create(int id)
    {
        return View(new AddProjectTaskDto { ProjectId =id});
    }
    [HttpPost]
    public async Task<IActionResult> Create(int id,AddProjectTaskDto addtask)
    {
        if (!ModelState.IsValid)
            return View(addtask);
        if (addtask.EndDate < addtask.StartDate)
        {
            ModelState.AddModelError("EndDate", "not match data");
            return View(addtask);
        }
        await _projectTaskServices.Add(addtask);
        return Redirect("/ProjectTask/Index/" + id);
    }
}
