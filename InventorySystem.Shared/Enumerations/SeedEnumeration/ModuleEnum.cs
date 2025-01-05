using InventorySystem.Shared.Definitions;
using Module = InventorySystem.Shared.Entities.Configuration.Identity.Module;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Contains predefined modules for the application.
    /// </summary>
    public static class ModuleEnum
    {
        public readonly static Module Application = new() { Id = 1, Name = ModuleDefinitions.Application, Description = "Application", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, ModifiedDate = DateTime.UtcNow, CreatedBy = "1" };

        /// <summary>
        /// Retrieves all predefined modules as a list.
        /// </summary>
        /// <returns>A list of all <see cref="Module"/> objects defined in this class.</returns>
        public static List<Module> GetAll()
        {
            return
            [
                Application
            ];
        }
    }
}
