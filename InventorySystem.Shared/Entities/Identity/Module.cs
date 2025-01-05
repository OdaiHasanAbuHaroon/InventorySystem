using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class Module : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        #region Collections

        public virtual ICollection<Feature> Features { get; set; } = new HashSet<Feature>();

        public virtual ICollection<UserModule> UserModules { get; set; } = new HashSet<UserModule>();

        #endregion
    }
}
