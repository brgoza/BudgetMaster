using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetMaster.Data.EntityTypes;

[Table("Budgets")]
public class Budget
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<BudgetCategory> BudgetCategories { get; set; } = new List<BudgetCategory>();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public double EstimatedDailyExpense { get; set; }
    public double EstimatedDailyIncome { get; set; }
    public double ActualDailyExpense { get; set; }
    public double ActualDailyIncome { get; set; }
    public Guid HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
    public ICollection<RecurringEvent> RecurringEvents { get; set; } = new List<RecurringEvent>();

}
