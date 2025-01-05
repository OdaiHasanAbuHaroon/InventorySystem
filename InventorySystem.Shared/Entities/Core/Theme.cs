using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Core
{
    public class Theme : BaseEntity
    {
        public required string Name { get; set; }

        public string? Color { get; set; }

        public int? FontSize { get; set; }

        public required bool IsDefault { get; set; } = false;

        #region Collections

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        #endregion
    }
}
