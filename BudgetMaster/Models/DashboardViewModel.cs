using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;

namespace BudgetMaster.Models;

public class DashboardViewModel
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; } 
    
    
    public List<AccountViewModel> AccountViewModels { get; set; } = new();
    public List<HouseholdViewModel>? Households { get; set; } 
    //public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    //public ICollection<Account> Accounts { get; set; } = new List<Account>();
    //public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    //public ICollection<HouseholdUser> HouseholdUsers { get; set; } = new List<HouseholdUser>();
}
