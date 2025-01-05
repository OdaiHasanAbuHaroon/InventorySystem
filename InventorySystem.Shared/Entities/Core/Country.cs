using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Core
{
    public class Country : BaseEntity
    {
        public required string Name { get; set; }

        public required string Code { get; set; }
    }
}
