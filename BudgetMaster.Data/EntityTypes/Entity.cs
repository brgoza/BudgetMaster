using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMaster.Data.EntityTypes
{
    [Table("Entities")]
    public class Entity
    {
        public Guid Id { get; set; }
        public Guid EntityTypeId { get; set; }
        public EntityType EntityType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ContactInfo ContactInfo { get; set; } = null!;
        public string WebUrl { get; set; } = null!;
        public string ApiUrl { get; set; } = null!;

    }

    public class ContactInfo
    {
        public string Phone { get; set; } = null!;
        public Address Address { get; set; } = null!;
    }
    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
    }
    [Table("EntityTypes")]
    public class EntityType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    [Table("BudgetEntities")]
    public class BudgetEntity
    {
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; } = null!;
        public Guid EntityId { get; set; }
        public Entity Entity { get; set; } = null!;
        public string? Comments { get; set; }
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }
    }



}