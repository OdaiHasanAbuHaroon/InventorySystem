using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class UserModule : MinEntity
    {
        public required long UserId { get; set; }
        public virtual User? User { get; set; }

        public required long ModuleId { get; set; }
        public virtual Module? Module { get; set; }
    }
}
