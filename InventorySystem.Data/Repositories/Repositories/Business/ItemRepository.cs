using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(DatabaseContext context) : base(context) { }
    }
}
