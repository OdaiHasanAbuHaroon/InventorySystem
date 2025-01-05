using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class Permission : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        #region Collections

        public ICollection<FeaturePermission> FeaturePermissions { get; set; } = new HashSet<FeaturePermission>();

        #endregion
    }
}
