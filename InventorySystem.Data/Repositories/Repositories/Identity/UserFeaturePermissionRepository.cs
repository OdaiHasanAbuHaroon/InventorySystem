using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class UserFeaturePermissionRepository : GenericRepository<UserFeaturePermission>, IUserFeaturePermissionRepository
    {
        public UserFeaturePermissionRepository(DatabaseContext context) : base(context) { }
    }
}
