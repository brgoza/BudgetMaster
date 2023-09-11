using System.ComponentModel.DataAnnotations;

using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.Identity.Client;

namespace BudgetMaster.Models;

public class HouseholdInvitation : IHouseholdNotificationEvent, IUserNotificationEvent
{
    public Guid Id { get; set; }
    public bool Viewed { get; set; }
    public string? InviteeEmail { get; set; } = null!;
    public Guid HouseholdId { get; set; }
    public Household Household { get; set; } = null!;
    public Guid InvitedById { get; set; }
    public AppUser InvitedBy { get; set; } = null!;
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public HouseholdRole HouseholdRole { get; set; } = HouseholdRole.Member;
    public string NotificationText => $"You have been invited to join the household {Household.Name} by {InvitedBy.UserName}";

    public HouseholdInvitation(string? email, Household household, AppUser sender, AppUser? invitee, HouseholdRole role = HouseholdRole.Member)
    {
        InviteeEmail = email ?? invitee?.Email;
        User = invitee;
        UserId = invitee?.Id ?? Guid.Empty;
        Household = household;
        HouseholdId = household.Id;
        InvitedBy = sender;
        InvitedById = sender.Id;
        HouseholdRole = role;
    }
}
