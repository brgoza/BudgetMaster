using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;
using BudgetMaster.Models.Accounts;
using BudgetMaster.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserService _userService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountsController(UserManager<AppUser> userManager, UserService userService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
            => View();

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result = await _signInManager.UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
            => View();


        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            AppUser? user = await _userService.GetAppUserByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return View("Login");
            }

            if (_signInManager.SignInAsync(user, model.RememberMe).IsCompletedSuccessfully)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View("Login");
            }
        }
    }
}

