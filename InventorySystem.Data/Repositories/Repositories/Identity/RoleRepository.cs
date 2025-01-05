using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DatabaseContext context) : base(context) { }
    }
}
