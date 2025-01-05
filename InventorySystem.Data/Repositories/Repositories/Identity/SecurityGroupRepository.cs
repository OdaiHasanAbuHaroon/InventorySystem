using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class SecurityGroupRepository : GenericRepository<SecurityGroup>, ISecurityGroupRepository
    {
        public SecurityGroupRepository(DatabaseContext context) : base(context) { }
    }
}
