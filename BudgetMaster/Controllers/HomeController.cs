using System.Diagnostics;

using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;
using BudgetMaster.Services;
using BudgetMaster.ModelHelpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly ModelHelperService _modelHelpers;
        public HomeController(ILogger<HomeController> logger, UserService userService, ModelHelperService model)
        {
            _logger = logger;
            _userService = userService;
            _modelHelpers = model;
        }

        public IActionResult Index()
        {
            if (User.Identity is null || !User.Identity!.IsAuthenticated)
                return RedirectToAction("Register", "Accounts");
            return RedirectToAction("Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            AppUser user = await GetCurrentUser();
            DashboardViewModel model = _modelHelpers.CreateDashboardViewModel(user);
            return View(model);
        }

     


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        private async Task<AppUser> GetCurrentUser()
        {
            string? username = (User.Identity?.Name) ?? throw new Exception("User is not logged in.");
            return await _userService.GetAppUserByNameAsync(username) ?? throw new Exception("User is not logged in.");
        }

    }
}
