using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Contains predefined roles for the application.
    /// </summary>
    public static class RoleEnum
    {
        public readonly static Role ApplicationOwner = new() { Id = 1, RoleName = RoleDefinitions.ApplicationOwner, RoleDescription = "Administrator role for Migration and database update", CreatedDate = DateTime.UtcNow, IsActive = false, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };
        public readonly static Role BackGroundService = new() { Id = 2, RoleName = RoleDefinitions.BackGroundService, RoleDescription = "User for background services", CreatedDate = DateTime.UtcNow, IsActive = false, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };
        public readonly static Role SuperAdministrator = new() { Id = 3, RoleName = RoleDefinitions.SuperAdministrator, RoleDescription = "Administrator role with full access", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };
        public readonly static Role Administrator = new() { Id = 4, RoleName = RoleDefinitions.Administrator, RoleDescription = "Administrator", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };
        public readonly static Role User = new() { Id = 5, RoleName = RoleDefinitions.User, RoleDescription = "System User", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };
        public readonly static Role Viewer = new() { Id = 6, RoleName = RoleDefinitions.Viewer, RoleDescription = "Viewer", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };

        /// <summary>
        /// Retrieves all predefined roles as a list.
        /// </summary>
        /// <returns>A list of all <see cref="Role"/> objects defined in this class.</returns>
        public static List<Role> GetAll()
        {
            return
            [
                SuperAdministrator,
                Administrator,
                User,
                Viewer
            ];
        }
    }
}
