using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(DatabaseContext context) : base(context) { }
    }
}
