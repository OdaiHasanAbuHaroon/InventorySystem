using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Identity;

namespace InventorySystem.Data.Repositories.Repositories.Identity
{
    public class TwofactorRepository : GenericRepository<Twofactor>, ITwofactorRepository
    {
        public TwofactorRepository(DatabaseContext context) : base(context) { }
    }
}
