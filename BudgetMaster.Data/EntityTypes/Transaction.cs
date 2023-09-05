using System.ComponentModel.DataAnnotations.Schema;

using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.Data.EntityTypes
{
    [Table("Transactions")]
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; } = null!;
        public Guid? BudgetEventId { get; set; }
        public BudgetEvent? BudgetEvent { get; set; }
        public Guid? BudgettItemId { get; set; }
        public RecurringEvent? BudgetItem { get; set; }
        [Precision(10, 2)]
        public decimal Amount { get; set; }
        public decimal BalanceChange => TransactionType.IsIncome ? Amount : -Amount;
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; } = null!;
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Guid? EntityId { get; set; }
        public Entity? Entity { get; set; }
        public string Comment { get; set; } = null!;

        public DateTime Date { get; set; }

    }
    [Table("TransactionTypes")]
    public class TransactionType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsIncome { get; set; }
        public bool IsExpense => !IsIncome;
    }
}