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

        public IActionResult Index()
        {
            return View();
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
