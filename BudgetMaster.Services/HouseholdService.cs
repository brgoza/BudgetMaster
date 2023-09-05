using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes;
using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetMaster.Services;

public class HouseholdService
{
    private readonly ILogger<HouseholdService> _logger;
    private readonly AppDbContext _dbContext;
    public HouseholdService(ILogger<HouseholdService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
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



}
