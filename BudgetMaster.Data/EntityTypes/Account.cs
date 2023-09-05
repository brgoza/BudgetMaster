using System.Collections.ObjectModel;

using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.Data.EntityTypes
{
    public  class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? EntityId { get; set; }
        public Entity? Entity { get; set; } = null!;
        [Precision(10, 2)]
        public decimal Balance { get; set; }
        public Guid? UserId { get; set; }
        public Household? Household { get; set; }
        [DeleteBehavior(DeleteBehavior.ClientSetNull)]
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public ICollection<AccountUser> AccountUsers { get; set; } = new List<AccountUser>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }


    [PrimaryKey(nameof(AccountId), nameof(AppUserId))]
    public class AccountUser
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public AccountUserPrivileges AccountPrivileges { get; set; }
    }

    [Flags]
    public enum AccountUserPrivileges
    {
        View,
        Deposit,
        FullAccess
    }
    public class  BankAccount : Account
    {
        public string? RoutingNumber { get; set; }
        public string? AccountNumber { get; set; }
    }
    public class CheckingAccount : BankAccount
    {
        

    }
    public class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; }
        public DateTime InterestDate { get; set; }

    }
    public class InvestmentAccount : Account
    {
      
    }
    public class CreditCardAccount : Account
    {
        public decimal CreditLimit { get; set; }
        public double InterestRate { get; set; }
        public DateTime InterestDate { get; set; }
        [Precision(10, 2)]
        public decimal MinimumPayment { get; set; }
    }
   

}