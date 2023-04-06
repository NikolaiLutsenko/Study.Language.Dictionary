using Lang.Dictionary.App.Models;
using Lang.Dictionary.App.Models.Users;
using Lang.Dictionary.App.Services;
using Lang.Dictionary.App.Settings;
using Lang.Dictionary.Web.Models.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lang.Dictionary.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly LanguageSettings _languageSettings;
        private readonly UserService _userService;

        public AccountsController(UserService userService, IOptions<LanguageSettings> languageSettings)
        {
            _languageSettings = languageSettings.Value;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var getUserResult = await _userService.GetUser(model.Email, model.Password);
            if (!getUserResult.IsSuccess)
            {
                ModelState.AddModelError("", getUserResult.Error);
                return View(model);
            }

            await InternalLoginAsync(getUserResult);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Languages = _languageSettings.Languages.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View(new RegisterViewModel { });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            ViewBag.Languages = _languageSettings.Languages.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.CreateUser(
                model.Name,
                model.Email,
                model.Password,
                model.BaseLanguageId.Value,
                model.StudyLanguageId.Value);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Такий користувач вже зареєстрован");
                return View(model);
            }

            await InternalLoginAsync(result);

            return RedirectToAction("Index", "Home");
        }

        private async Task InternalLoginAsync(Result<UserModel> result)
        {
            var user = result.Value;

            var claims = new List<Claim>
            {
                new Claim("Id", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("BaseLanguageId", user.UserSettings.BaseLanguage.Id.ToString()),
                new Claim("StudyLanguageId", user.UserSettings.StudyLanguage.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
