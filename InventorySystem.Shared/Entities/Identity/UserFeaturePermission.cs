using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class UserFeaturePermission : MinEntity
    {
        public required long UserId { get; set; }
        public virtual User? User { get; set; }

        public required long FeaturePermissionId { get; set; }
        public virtual FeaturePermission? FeaturePermission { get; set; }
    }
}
