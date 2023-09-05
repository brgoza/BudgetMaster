using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;

namespace BudgetMaster.ModelHelpers;

public  class ModelHelperService
{
    public  DashboardViewModel CreateDashboardViewModel(AppUser user)
    {
        DashboardViewModel model = new()
        {
            Households = new ()
        };
        foreach (var household in user.Households)
        {
            HouseholdViewModel householdViewModel = CreateHouseholdViewModel(household);
            model.Households.Add(householdViewModel);
        }
        return model;
    }
    
    public HouseholdsViewModel CreateHouseholdsViewModel(List<Household> households)
    {
        HouseholdsViewModel model = new()
        {
            Households = new List<HouseholdViewModel>()
        };
        foreach (var household in households)
        {
            HouseholdViewModel householdViewModel = CreateHouseholdViewModel(household);
            model.Households.Add(householdViewModel);
        }
        return model;
    }
    public HouseholdViewModel CreateHouseholdViewModel(Household household)
    {
        HouseholdViewModel model = new()
        {
            HouseholdId = household.Id,
            HouseholdName = household.Name,
            HouseholdDescription = household.Description,
            HouseholdUsers = new List<HouseholdUserViewModel>()
        };
        foreach (var householdUser in household.HouseholdUsers)
        {
            HouseholdUserViewModel householdUserViewModel = new()
            {
                UserId = householdUser.AppUserId,
                UserName = householdUser.AppUser.UserName ??
                    householdUser.AppUser.Email ??
                    householdUser.AppUser.Id.ToString(),
                Role = householdUser.Role
            };
            model.HouseholdUsers.Add(householdUserViewModel);
        }
        return model;
    }
}
