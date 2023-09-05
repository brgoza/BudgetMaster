using System.ComponentModel.DataAnnotations.Schema;

using BudgetMaster.Data.EntityTypes.Identity;

namespace BudgetMaster.Data.EntityTypes;

[Table("Households")]
public class Household
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<HouseholdUser> HouseholdUsers { get; set; } = new List<HouseholdUser>();
}



[Table("HouseholdUsers")]
public class HouseholdUser
{
    public Guid HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public HouseholdUserRole Role { get; set; } 
}



public enum HouseholdUserRole
{
    Owner,
    Admin,
    Member,
    Viewer
}
