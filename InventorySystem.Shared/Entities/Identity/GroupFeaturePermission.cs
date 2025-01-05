using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class GroupFeaturePermission : MinEntity
    {
        public required long SecurityGroupId { get; set; }
        public virtual SecurityGroup? SecurityGroup { get; set; }

        public required long FeaturePermissionId { get; set; }
        public virtual FeaturePermission? FeaturePermission { get; set; }
    }
}
