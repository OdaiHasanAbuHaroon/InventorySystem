using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    public static class SecurityGroupEnum
    {
        public readonly static SecurityGroup SuperAdminGroup = new() { Id = 1, Name = "SuperAdminGroup", IsActive = true, IsDeleted = false, Description = "Super Admin Group", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow };
        public readonly static SecurityGroup AdminGroup = new() { Id = 2, Name = "AdminGroup", IsActive = true, IsDeleted = false, Description = "Admin Group", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow };
        public readonly static SecurityGroup UserGroup = new() { Id = 3, Name = "UserGroup", IsActive = true, IsDeleted = false, Description = "UserGroup", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow };

        /// <summary>
        /// Retrieves all predefined Security Groups as a list.
        /// </summary>
        /// <returns>A list of all <see cref="SecurityGroup"/> objects defined in this class.</returns>
        public static List<SecurityGroup> GetAll()
        {
            return
            [
                SuperAdminGroup,
                AdminGroup,
                UserGroup
            ];
        }
    }
}
