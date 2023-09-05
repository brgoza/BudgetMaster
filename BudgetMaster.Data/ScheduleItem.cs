using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;

namespace BudgetMaster.Data;

public class ScheduledEvent
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public bool IsIncome { get; set; }
    public bool IsExpense => !IsIncome;
    public Guid? EntityId { get; set; }
    public Entity? Entity { get; set; } 
    public string? Comment { get; set; }
    public Guid EventTypeId { get; set; }
    public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    public Guid CreatedById { get; set; }
    public AppUser CreatedBy { get; set; } = null!;
    public DateTime? UpdatedOn { get; set; }
    
   
    public Guid HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
}
