using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BudgetMaster.Data.EntityTypes.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Household> Households { get; set; } = new List<Household>();
    public ICollection<HouseholdUser> HouseholdUsers { get; set; } = new List<HouseholdUser>();
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<AccountUser> UserAccounts { get; set; } = new List<AccountUser>();
}

