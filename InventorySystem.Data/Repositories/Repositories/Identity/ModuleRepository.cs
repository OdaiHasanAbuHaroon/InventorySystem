using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class ModuleRepository : GenericRepository<Shared.Entities.Configuration.Identity.Module>, IModuleRepository
    {
        public ModuleRepository(DatabaseContext context) : base(context) { }
    }
}
