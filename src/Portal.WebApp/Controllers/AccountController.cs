using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Users;
using Portal.Domain.Users.Contracts;
using Portal.Domain.Users.DTOs;
using System.Security.Claims;

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

        [Route("login"), HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logIn)
        {
            if (!ModelState.IsValid)
                return View(logIn);
            var user = await _userService.LogIn(logIn);
            if (user == null)
                ModelState.AddModelError("UserName", "Not valid user");
            else
            {
                await CreateCookieAuth(user);
                TempData["msgSuccess"] = $"Welcome {user.FirstName+ " " + user.LastName}";
                return Redirect("/");
            }
            return View(logIn);
        }
        private async Task CreateCookieAuth(User user)
        {

            var claims = new List<Claim>()
            {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(30),
            };
            await HttpContext.SignInAsync(principal, properties);
        }
        [Route("/Logout"), Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

