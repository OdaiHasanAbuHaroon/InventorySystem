using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class SecurityGroup : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        #region Collections

        public virtual ICollection<UserSecurityGroup> UserSecurityGroups { get; set; } = new HashSet<UserSecurityGroup>();

        public virtual ICollection<GroupFeaturePermission> GroupFeaturePermissions { get; set; } = new HashSet<GroupFeaturePermission>();

        #endregion
    }
}
