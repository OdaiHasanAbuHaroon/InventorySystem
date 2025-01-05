using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class UserModuleRepository : GenericRepository<UserModule>, IUserModuleRepository
    {
        public UserModuleRepository(DatabaseContext context) : base(context) { }
    }
}
