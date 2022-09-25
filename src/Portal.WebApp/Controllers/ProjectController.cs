﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.ApplicationServices.Common;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;
using System.Security.Claims;
namespace Portal.WebApp.Controllers;

[Authorize]
public class ProjectController : Controller
{
    private readonly ProjectServices _projectServices;
    private readonly ProjectTaskServices _projectTaskServices;
    public ProjectController(ProjectServices projectServices,
        ProjectTaskServices projectTaskServices)
    {
        _projectServices = projectServices;
        _projectTaskServices = projectTaskServices;
    }

    #region Get all projects
    public async Task<IActionResult> Index(int pageId = 1, int take = 4, string search = "")
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var projects = await _projectServices
            .GetAll(userId, pageId, take, search);
        return View(projects);
    }
    #endregion

    #region Create Project

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddProjectDto projectDto)
    {
        if (!ModelState.IsValid)
            return View(projectDto);
        if (projectDto.EndDate < projectDto.StartDate)
        {
            ModelState.AddModelError("EndDate", "not match data");
            return View(projectDto);
        }
        string image = string.Empty;
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (projectDto.File != null)
        {
            image = UploadImage(projectDto.File);
        }
        await _projectServices.Add(projectDto, userId, image);
        return Redirect(nameof(ProjectController.Index));
    }
    #endregion

    #region Edit Project

    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectServices.GetById(id);
        return View(new EditProjectDto
        {
            Image = project.Image,
            EndDate = project.EndDate,
            PriceTask = project.PriceTask,
            StartDate = project.StartDate,
            Title = project.Title,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditProjectDto projectDto)
    {
        if (!ModelState.IsValid)
            return View(projectDto);
        if (projectDto.EndDate < projectDto.StartDate)
        {
            ModelState.AddModelError("EndDate", "not match data");
            return View(projectDto);
        }
        if (projectDto.File != null)
        {
            projectDto.Image = UploadImage(projectDto.File);
        }
        await _projectServices.Update(id, projectDto);
        return Redirect("/Project");
    }
    #endregion


    private string UploadImage(IFormFile file)
    {
        return UploadHelper.Upload(file, "wwwroot");

    }








}
