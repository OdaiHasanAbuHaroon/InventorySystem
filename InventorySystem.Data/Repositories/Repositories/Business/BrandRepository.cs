using InventorySystem.Shared.Entities.Business;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Business;

namespace InventorySystem.Data.Repositories.Repositories.Business
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DatabaseContext context) : base(context) { }
    }
}
