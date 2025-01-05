using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Core
{
    public class Person : BaseEntity
    {
        public required string Name { get; set; }

        public required string Reference { get; set; }

        public required string Type { get; set; }

        public required decimal Cost { get; set; }

        public required long UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
