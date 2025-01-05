using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Business
{
    public class Brand : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required long ManufacturerId { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
    }
}