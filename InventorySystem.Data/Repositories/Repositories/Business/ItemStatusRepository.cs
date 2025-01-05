using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class ItemStatusRepository : GenericRepository<ItemStatus>, IItemStatusRepository
    {
        public ItemStatusRepository(DatabaseContext context) : base(context) { }
    }
}
