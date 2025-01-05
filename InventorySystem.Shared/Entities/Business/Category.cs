using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Business
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}