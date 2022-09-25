using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;

namespace Portal.WebApp.Controllers;

[Authorize]
public class ProjectTaskController : Controller
{
    private readonly ProjectTaskServices _projectTaskServices;
    private readonly ProjectServices _projectServices;
    public ProjectTaskController(ProjectTaskServices projectTaskServices,
        ProjectServices projectServices)
    {
        _projectTaskServices = projectTaskServices;
        _projectServices = projectServices;
    }

    #region Get All Tasks
    public async Task<IActionResult> Index(int projectId)
    {
        var project = await _projectServices.GetById(projectId);
        if (project.IsEnd || project.EndDate < DateTime.Now)
        {
            TempData["msgDanger"] = $"{project.Title} end project";
            return Redirect("/Project");
        }
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
            Title = task.Title,

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

    #region Remove Task
    public async Task<IActionResult> Remove(int id)
    {
        var task = await _projectTaskServices.GetById(id);
        var project = await _projectServices.GetById(task.ProjectId);
        if (project.IsEnd || project.EndDate < DateTime.Now)
        {
            TempData["msgDanger"] = $"{project.Title} end project";
            return Redirect("/Project");
        }
        return View(task);
    }

    public async Task<IActionResult> ConfirmRemove(int id, int projectId)
    {
        var result = await _projectTaskServices.Remove(id);
        if (!result.IsSucces)
            TempData["msgDanger"] = result.Message;
        else
            TempData["msgSuccess"] = result.Message;
        return RedirectToAction(nameof(ProjectTaskController.Index),
            new { projectId = projectId });
    }
    #endregion

}
