using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class Role : BaseEntity
    {
        public required string RoleName { get; set; }

        public string? RoleDescription { get; set; }

        #region Collections

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        #endregion
    }
}
