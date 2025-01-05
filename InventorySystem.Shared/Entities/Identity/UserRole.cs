using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Configuration
{
    public class UserRole : MinEntity
    {
        public required long RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public required long UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
