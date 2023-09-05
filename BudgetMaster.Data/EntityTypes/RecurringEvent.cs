using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Common;
using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.Data.EntityTypes;

[Table("RecurringEvents")]
public class RecurringEvent
{

    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; } = null!;
    public Guid BudgetCategoryId { get; set; }
    public BudgetCategory BudgetCategory { get; set; } = null!;
    public Guid? EntityId { get; set; }
    public Entity? Entity { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [NotMapped]
    public BudgetEvent? LastEvent { get; set; }
    [NotMapped]
    public BudgetEvent? NextEvent { get; set; }
    [Precision(10, 2)]
    public decimal ExpectedPeriodicAmount { get; set; }
    [Precision(10, 2)]
    public decimal? ActualPeriodicAmount { get; set; }

    public Frequency Frequency { get; set; }

    public ICollection<BudgetEvent> BudgetEvents { get; set; } = new List<BudgetEvent>();

}

[Table("BudgetCategories")]
public class BudgetCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsIncome { get; set; }
    public bool IsExpense => !IsIncome;
}

public class BudgetSubCategory
{
    public Guid Id { get; set; }
    public Guid BudgetCategoryId { get; set; }
    public BudgetCategory BudgetCategory { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}