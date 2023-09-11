using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Common;
using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetMaster.Services;

public class HouseholdService
{
    private readonly ILogger<HouseholdService> _logger;
    private readonly UserService _userService;
    private readonly AppDbContext _dbContext;
    public HouseholdService(ILogger<HouseholdService> logger, AppDbContext dbContext, UserService userService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userService = userService;
    }
    public async Task<Household?> GetHouseholdAsync(Guid householdId)
        => await _dbContext.Households.FindAsync(householdId);
    public async Task<List<Household>> GetUserHouseholdsAsync(AppUser user)
        => await _dbContext.Households
                    .Include(u => u.HouseholdUsers)
                    .Include(u => u.Budgets)
                    .Include(u => u.Accounts)
                    .Where(h => h.HouseholdUsers.Any(hu => hu.AppUserId == user.Id))
                    .ToListAsync();
    public async Task<List<Household>> GetUserHouseholdsAsync(Guid userId)
          => await _dbContext.Households
                    .Include(u => u.HouseholdUsers)
                    .Include(u => u.Budgets)
                    .Include(u => u.Accounts)
                    .Where(h => h.HouseholdUsers.Any(hu => hu.AppUserId == userId))
                    .ToListAsync();

    private HouseholdUser? GetHouseholdUser(Guid userId, Guid householdId) => _dbContext.HouseholdUsers
        .Include(hu => hu.Role)
        .Where(hu => hu.HouseholdId == householdId && hu.AppUserId == userId).FirstOrDefault();
    public List<HouseholdUser> GetHouseholdUsers(Guid householdId) => _dbContext.HouseholdUsers
        .Include(hu => hu.Role)
        .ToList();
    public async Task<Result> SendInvite(Guid userId, Guid householdId, string email)
    {
        Result result = await ValidateHouseholdInvitation(userId, householdId, email);
        if (result.IsFailure)
            return result;
        HouseholdInvitation invite = (result as Result<HouseholdInvitation>)!.Data!;
        Result saveResult = await SaveInvitation(invite);
        return saveResult;
    }
    private async Task<Result> SaveInvitation(HouseholdInvitation invitation)
    {
        try
        {
            await _dbContext.HouseholdInvitations.AddAsync(invitation);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error saving invitation");
            return Result.Failure("Error saving invitation");
        }
    }
    private async Task<Result> ValidateHouseholdInvitation(Guid userId, Guid householdId, string email)
    {

        AppUser? inviter = await _userService.GetAppUserAsync(userId);
        if (inviter is null)
            return Result.Failure("User not found");

        Household? household = await GetHouseholdAsync(householdId);
        if (household is null)
            return Result.Failure("Household not found");

        HouseholdUser? householdUser = GetHouseholdUser(userId, householdId);
        if (householdUser is null)
            return Result.Failure("Logged in user not in household");

        if (householdUser.Role != HouseholdRole.Owner && householdUser.Role != HouseholdRole.Admin)
            return Result.Failure("Logged in User not authorized to invite");

        AppUser? invitee = await _userService.GetAppUserByEmailAsync(email);
        if (invitee is null)
            return Result.Failure("User not found"); //TODO: SEND EMAIL

        if (household.HouseholdUsers.Any(hu => hu.AppUserId == invitee.Id))
            return Result.Failure("User already in household");
        HouseholdInvitation invite = new(email, household, inviter, invitee);
        return Result<HouseholdInvitation>.Success(invite);
    }
}
