using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models.Accounts;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class AccountsController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        public AccountsController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
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
    }
}
