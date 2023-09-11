using BudgetMaster.Common;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.ModelHelpers;
using BudgetMaster.Models;
using BudgetMaster.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class HouseholdsController : Controller
    {
        private readonly ILogger<HouseholdsController> _logger;
        private readonly UserService _userService;
        private readonly ModelHelperService _modelHelpers;
        public HouseholdsController(ILogger<HouseholdsController> logger, UserService userService, ModelHelperService modelHelpers)
        {
            _logger = logger;
            _userService = userService;
            _modelHelpers = modelHelpers;
        }
        [Authorize, HttpGet]
        public IActionResult Index()
        {
            HouseholdsViewModel model = _modelHelpers.CreateHouseholdsViewModel(GetCurrentUser);
            return View(model);
        }
        [HttpPost, Authorize]
        public Result CreateHousehold([FromBody] HouseholdViewModel model)
        {
            throw new NotImplementedException();
        }
        public Result Invite(Guid householdId, string email)
        {
            throw new NotImplementedException();
        }
        private AppUser GetCurrentUser => _userService.GetAppUserByNameAsync(User.Identity?.Name ?? throw new Exception("User is not logged in.")).Result ?? throw new Exception("User is not logged in.");
    }
}
