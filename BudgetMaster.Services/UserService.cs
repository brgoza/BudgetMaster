using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetMaster.Data.EntityTypes.Identity;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Identity.Core;
using BudgetMaster.Common;


namespace BudgetMaster.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<AppUser> _userManager;
    
    public UserService(ILogger<UserService> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    public async Task<AppUser?> GetAppUserAsync(string userId)
         => await _userManager.FindByIdAsync(userId);
    public async Task<AppUser?> GetAppUserAsync(Guid userId)
                => await _userManager.FindByIdAsync(userId.ToString());
    public async Task<AppUser?> GetAppUserByNameAsync(string username)
        => await _userManager.FindByNameAsync(username);
}
