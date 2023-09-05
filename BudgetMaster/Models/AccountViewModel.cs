using BudgetMaster.Data.EntityTypes;

namespace BudgetMaster.Models
{
    public class AccountViewModel
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public double Balance { get; set; }
        public string AccountType { get; set; } = null!;
        public Guid? EntityId { get; set; }
        public Entity? Entity { get; set; }
    }
}
