using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class UserSecurityGroupRepository : GenericRepository<UserSecurityGroup>, IUserSecurityGroupRepository
    {
        public UserSecurityGroupRepository(DatabaseContext context) : base(context) { }
    }
}
