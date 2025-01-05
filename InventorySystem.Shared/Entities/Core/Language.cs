using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Core
{
    public class Language : BaseEntity
    {
        public required string Name { get; set; }

        public required string NativeName { get; set; }

        public required string Code { get; set; }

        public string? NameAr { get; set; }

        #region Collections

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        #endregion
    }
}
