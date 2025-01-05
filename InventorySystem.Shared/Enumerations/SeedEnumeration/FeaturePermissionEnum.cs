using InventorySystem.Shared.Entities.Configuration.Identity;
using System.Reflection;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Contains predefined feature permissions for the application, grouped by features (e.g., Icon).
    /// </summary>
    public static class FeaturePermissionEnum
    {
        #region Icon Permissions

        public readonly static FeaturePermission Icon_View = new() { Id = 1, FeatureId = FeatureEnum.Icon.Id, PermissionId = PermissionEnum.View.Id, Feature = FeatureEnum.Icon };

        public readonly static FeaturePermission Icon_Create = new() { Id = 2, FeatureId = FeatureEnum.Icon.Id, PermissionId = PermissionEnum.Create.Id, Feature = FeatureEnum.Icon };

        public readonly static FeaturePermission Icon_Edit = new() { Id = 3, FeatureId = FeatureEnum.Icon.Id, PermissionId = PermissionEnum.Edit.Id, Feature = FeatureEnum.Icon };

        public readonly static FeaturePermission Icon_Details = new() { Id = 4, FeatureId = FeatureEnum.Icon.Id, PermissionId = PermissionEnum.Details.Id, Feature = FeatureEnum.Icon };

        public readonly static FeaturePermission Icon_Delete = new() { Id = 5, FeatureId = FeatureEnum.Icon.Id, PermissionId = PermissionEnum.Delete.Id, Feature = FeatureEnum.Icon };

        #endregion

        /// <summary>
        /// Retrieves all feature permissions with their names as dictionary keys.
        /// </summary>
        /// <returns>A dictionary where the key is the name of the permission and the value is the corresponding <see cref="FeaturePermission"/>.</returns>
        public static Dictionary<string, FeaturePermission> GetAllWithNames()
        {
            return typeof(FeaturePermissionEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(FeaturePermission))
                .ToDictionary(field => field.Name, field => (FeaturePermission)field.GetValue(null)!);
        }

        /// <summary>
        /// Retrieves all feature permissions as a list.
        /// </summary>
        /// <returns>A list of all <see cref="FeaturePermission"/> objects.</returns>
        public static List<FeaturePermission> GetAll()
        {
            return
            [
                Icon_View ,
                Icon_Create ,
                Icon_Edit ,
                Icon_Details ,
                Icon_Delete
            ];
        }
    }
}
