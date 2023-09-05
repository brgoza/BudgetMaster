using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.EntityFrameworkCore;

namespace BudgetMaster.Data.EntityTypes;
[Table("Events")]
public class BudgetEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid EventTypeId { get; set; }
    public EventType EventType { get; set; } = null!;
    public List<EventLog> EventLogs { get; set; } = new();
    [Precision(10, 2)]
    public decimal Amount { get; set; }
    public Guid BudgetCategoryId { get; set; }
    public BudgetCategory BudgetCategory { get; set; } = null!;
    public Guid? BudgetSubCategoryId { get; set; }
    public BudgetSubCategory? BudgetSubCategory { get; set; }

    public ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    public Guid? RecurringEventId { get; set; }
    public RecurringEvent? RecurringEvent { get; set; }

    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualTime { get; set; }
    public int EventStatusId { get; set; }
    public EventStatus EventStatus { get; set; } = null!;

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; } = null!;

    public AppUser? CreateddBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

[Table("EventTypes")]
public class EventType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsIncome { get; set; }
    public bool IsExpense => !IsIncome;

}

[Table("EventStatuses")]
public class EventStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

[Table("EventLogs")]
public class EventLog
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public int EventStatusId { get; set; }
    public EventStatus EventStatus { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public AppUser CreatedBy { get; set; } = null!;
}