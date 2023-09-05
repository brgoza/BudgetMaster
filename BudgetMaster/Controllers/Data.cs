using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.ModelHelpers;
using BudgetMaster.Models;
using BudgetMaster.Services;

using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class Data : Controller
    {
        private readonly ILogger<Data> _logger;
        private readonly HouseholdService _householdService;
        private readonly ModelHelperService _modelHelper;
        private readonly UserService _userService;
        public Data(ILogger<Data> logger, HouseholdService householdService, ModelHelperService modelHelperService, UserService userService)
        {

            _logger = logger;
            _householdService = householdService;
            _modelHelper = modelHelperService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<HouseholdsViewModel> GetUserHouseholdsAsync()
        {
            AppUser user = await GetCurrentUserAsync();
            List<Household> households = await _householdService.GetUserHouseholdsAsync(user);
            HouseholdsViewModel model = _modelHelper.CreateHouseholdsViewModel(households);
            return model;
        }


        [NonAction]
        private async Task<AppUser> GetCurrentUserAsync()
        {
            string? username = (User.Identity?.Name) ?? throw new Exception("User is not logged in.");
            return await _userService.GetAppUserByNameAsync(username) ?? throw new Exception("User is not logged in.");
        }
    }
}
