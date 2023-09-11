using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetMaster.Data;

public interface INotificationEvent
{
    public Guid Id { get; set; }
    public string NotificationText { get; }
    public bool Viewed { get; set; }
}
public interface IHouseholdNotificationEvent : INotificationEvent
{
    public Guid HouseholdId { get; set; }
}
public interface IUserNotificationEvent : INotificationEvent
{
    public Guid UserId { get; set; }
}
public interface IPostNotificationEvent : INotificationEvent
{
    public Guid PostId { get; set; }
}