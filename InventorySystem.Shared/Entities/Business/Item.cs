using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Business
{
    public class Item : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required string UnitOfMeasurement { get; set; }

        public required bool Serialized { get; set; }

        public required long CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public required long BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        public required long LocationId { get; set; }
        public virtual Location? Location { get; set; }

        public required long ItemStatusId { get; set; }
        public virtual ItemStatus? ItemStatus { get; set; }
    }
}
