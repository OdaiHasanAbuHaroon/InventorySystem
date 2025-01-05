using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    public static class FeatureEnum
    {
        public static Feature Icon { get; set; } = new Feature() { Id = 104, IsActive = true, ModuleId = ModuleEnum.Application.Id, Name = FeatureDefinitions.Icon, CreatedBy = "1", CreatedDate = DateTime.UtcNow, Description = "*", IsDeleted = false };

        /// <summary>
        /// Retrieves all predefined Features as a list.
        /// </summary>
        /// <returns>A list of all <see cref="Feature"/> objects defined in this class.</returns>
        public static List<Feature> GetAll()
        {
            return
            [
                Icon
            ];
        }
    }
}
