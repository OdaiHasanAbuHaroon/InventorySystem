using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class FeaturePermission : MinEntity
    {
        public long Id { get; set; }

        public required long FeatureId { get; set; }
        public virtual Feature? Feature { get; set; }

        public required long PermissionId { get; set; }
        public virtual Permission? Permission { get; set; }

        #region Collections

        public ICollection<UserFeaturePermission> UserFeaturePermissions { get; set; } = new HashSet<UserFeaturePermission>();

        public ICollection<GroupFeaturePermission> GroupFeaturePermissions { get; set; } = new HashSet<GroupFeaturePermission>();

        #endregion

    }
}
