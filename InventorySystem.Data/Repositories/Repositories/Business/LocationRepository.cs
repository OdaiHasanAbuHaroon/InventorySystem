using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(DatabaseContext context) : base(context) { }
    }
}
