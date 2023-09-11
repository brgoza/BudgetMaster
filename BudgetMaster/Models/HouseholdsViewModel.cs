using BudgetMaster.Data.EntityTypes;

namespace BudgetMaster.Models;

public class  HouseholdsViewModel
{
    public List<HouseholdViewModel>? Households { get; set; }
}
public class HouseholdViewModel
{
    public Guid UserId { get; set; }
    public Guid HouseholdId { get; set; }
    public string HouseholdName { get; set; } = null!;
    public string HouseholdDescription { get; set; } = null!;
    public List<HouseholdUserViewModel> HouseholdUsers { get; set; } = null!;
}

public class HouseholdUserViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public HouseholdRole Role { get; set; }
    public string RoleName => Role.ToString();
}

