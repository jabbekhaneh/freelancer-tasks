using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Users.Contracts;
using Portal.Domain.Users.DTOs;

namespace Portal.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("register"), HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
                return View(register);
            var result = await _userService.Register(register);
            
            if (result.IsSuccess)
            {
                TempData["msgSuccess"] = result.Message;
                return Redirect("/login");
            }
            TempData["msgDanger"] = result.Message;
            return View(register);
        }
        [Route("login")]
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
