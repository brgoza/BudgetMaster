using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes;

namespace BudgetMaster.Services;

public class RecurringEventService
{
    private readonly AppDbContext _context;
    public RecurringEventService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public DateTime GetNextOccurance(DateTime startDate, Frequency frequency)
        => frequency switch
        {
            Frequency.Daily => startDate.AddDays(1),
            Frequency.Weekly => startDate.AddDays(7),
            Frequency.BiWeekly => startDate.AddDays(14),
            Frequency.Monthly => startDate.AddMonths(1),
            Frequency.BiMonthly => startDate.AddMonths(2),
            Frequency.Quarterly => startDate.AddMonths(3),
            Frequency.SemiAnnually => startDate.AddMonths(6),
            Frequency.Yearly => startDate.AddYears(1),
            _ => startDate.AddDays(1)
        };
    public DateTime GetNextOccurance(DateTime startDate, int days)
        => startDate.AddDays(days);
    
    
}
