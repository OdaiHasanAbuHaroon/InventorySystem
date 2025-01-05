using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Contains predefined permissions for the application, categorized into different groups for better organization.
    /// </summary>
    public static class PermissionEnum
    {
        #region Basic Permissions

        public readonly static Permission View = new() { Id = 1, Name = PermissionDefinitions.View, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission Create = new() { Id = 2, Name = PermissionDefinitions.Create, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission Edit = new() { Id = 3, Name = PermissionDefinitions.Edit, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission Details = new() { Id = 4, Name = PermissionDefinitions.Details, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission Delete = new() { Id = 5, Name = PermissionDefinitions.Delete, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        #endregion

        #region Attachment Permissions

        public readonly static Permission ViewAttachment = new() { Id = 10, Name = PermissionDefinitions.ViewAttachment, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission UploadAttachment = new() { Id = 11, Name = PermissionDefinitions.UploadAttachment, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission DownloadAttachment = new() { Id = 12, Name = PermissionDefinitions.DownloadAttachment, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission DeleteAttachment = new() { Id = 13, Name = PermissionDefinitions.DeleteAttachment, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        #endregion

        #region Other Permissions

        public readonly static Permission ChangeEmailStatus = new() { Id = 20, Name = PermissionDefinitions.ChangeEmailStatus, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission ChangeGroup = new() { Id = 21, Name = PermissionDefinitions.ChangeGroup, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        public readonly static Permission ChangePassword = new() { Id = 22, Name = PermissionDefinitions.ChangePassword, IsActive = true, IsDeleted = false, Description = "*", CreatedBy = "1", CreatedDate = DateTime.UtcNow };

        #endregion

        /// <summary>
        /// Retrieves all predefined permissions as a list.
        /// </summary>
        /// <returns>A list of all <see cref="Permission"/> objects defined in this class.</returns>
        public static List<Permission> GetAll()
        {
            return
            [
                ChangeEmailStatus,
                ChangeGroup,
                ChangePassword,
                Create,
                Delete,
                DeleteAttachment,
                Details,
                DownloadAttachment,
                Edit,
                UploadAttachment,
                View,
                ViewAttachment
            ];
        }
    }
}
