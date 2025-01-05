using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Business
{
    public class Location : BaseEntity
    {
        public required string Name { get; set; }

        public required string Type { get; set; }

        public string? Address { get; set; }

        public long? ParentLocationId { get; set; }
    }
}
