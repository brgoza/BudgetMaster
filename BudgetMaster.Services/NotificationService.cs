using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace BudgetMaster.Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly EmailService _emailService;
    private readonly UserService _userService;
    private readonly HouseholdService _householdService;
    private readonly AppDbContext _dbContext;

    public NotificationService(ILogger<NotificationService> logger, EmailService emailService, UserService userService, HouseholdService householdService, AppDbContext dbContext)
    {
        _logger = logger;
        _emailService = emailService;
        _userService = userService;
        _householdService = householdService;
        _dbContext = dbContext;
    }

    public List<INotificationEvent> GetUserNotifications(AppUser user)
        => GetUserNotifications(user.Id);
        
    
    public List<INotificationEvent> GetUserNotifications(Guid userId)
    {
        List<INotificationEvent> events = new();
        IQueryable<HouseholdInvitation> invitations = _dbContext.HouseholdInvitations
            .Where(hi => hi.UserId == userId);
        events.AddRange(invitations);
        return events;
    }
    public List<INotificationEvent> GetHouseholdNotifications(Guid householdId)
    {
        List<INotificationEvent> events = new();
        IQueryable<HouseholdInvitation> invitations = _dbContext.HouseholdInvitations
            .Where(hi => hi.HouseholdId == householdId);
        events.AddRange(invitations);
        return events;
    }
    public IQueryable<INotificationEvent> GetUserNotificationsQuery(Guid userId) => _dbContext.HouseholdInvitations
        .Where(hi => hi.UserId == userId);
    public int GetUserNotificationCount(Guid userId)
        => GetUserNotificationsQuery(userId).Count();
    
}
