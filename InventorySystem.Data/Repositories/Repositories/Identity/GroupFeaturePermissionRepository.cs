using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class GroupFeaturePermissionRepository : GenericRepository<GroupFeaturePermission>, IGroupFeaturePermissionRepository
    {
        public GroupFeaturePermissionRepository(DatabaseContext context) : base(context) { }
    }
}
