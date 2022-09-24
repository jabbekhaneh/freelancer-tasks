using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.ApplicationServices.Common;
using Portal.Domain.Projects.Contracts;
using Portal.Domain.Projects.DTOs;
using System.Security.Claims;
namespace Portal.WebApp.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectServices _projectServices;
        public ProjectController(ProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        public async Task<IActionResult> Index(int pageId = 1, int take = 4, string search = "")
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var projects = await _projectServices
                .GetAll(userId, pageId, take, search);
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProjectDto projectDto)
        {
            if (!ModelState.IsValid)
                return View(projectDto);
            if(projectDto.EndDate < projectDto.StartDate)
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

        private string UploadImage(IFormFile file)
        {
            return UploadHelper.Upload(file, "wwwroot");

        }


    }

}
