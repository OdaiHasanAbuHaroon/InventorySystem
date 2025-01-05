using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class Feature : BaseEntity
    {
        public required long ModuleId { get; set; }
        public virtual Module? Module { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        #region Collections

        public ICollection<FeaturePermission> FeaturePermissions { get; set; } = new HashSet<FeaturePermission>();

        #endregion
    }
}
