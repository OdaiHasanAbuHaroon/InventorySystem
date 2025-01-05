using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class SerialNumberRepository : GenericRepository<SerialNumber>, ISerialNumberRepository
    {
        public SerialNumberRepository(DatabaseContext context) : base(context) { }
    }
}
