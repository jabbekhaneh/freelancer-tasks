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

    #region Get All Tasks
    public async Task<IActionResult> Index(int projectId)
    {
        var projectTasks = await _projectTaskServices.GetAllByProjectId(projectId);
        return View(projectTasks);
    }
    #endregion

    #region Create Task
    public IActionResult Create(int projectId)
    {
        return View(new AddProjectTaskDto { ProjectId = projectId });
    }
    [HttpPost]
    public async Task<IActionResult> Create(int projectId, AddProjectTaskDto addtask)
    {
        if (!ModelState.IsValid)
            return View(addtask);
        if (addtask.EndDate < addtask.StartDate)
        {
            ModelState.AddModelError("EndDate", "not match data");
            return View(addtask);
        }
        await _projectTaskServices.Add(addtask);
        return RedirectToAction(nameof(ProjectTaskController.Index), new { projectId = projectId });
    }
    #endregion

    #region Edit Task
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _projectTaskServices.GetById(id);
        return View(new EditProjectTaskDto
        {
            ProjectId = task.ProjectId,
            EndDate = task.EndDate,
            StartDate = task.StartDate,
            Title=task.Title,
            
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditProjectTaskDto editProjectTask)
    {
        if (!ModelState.IsValid)
            return View(editProjectTask);
        if (editProjectTask.EndDate < editProjectTask.StartDate)
        {
            ModelState.AddModelError("EndDate", "not match data");
            return View(editProjectTask);
        }
        await _projectTaskServices.Update(id, editProjectTask);
        return RedirectToAction(nameof(ProjectTaskController.Index), new { projectId = editProjectTask.ProjectId });
    }
    #endregion

}
