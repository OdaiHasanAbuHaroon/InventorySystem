using InventorySystem.Shared.Entities.Core;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Core;

namespace InventorySystem.Data.Repositories.Repositories.Core
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext context) : base(context) { }
    }
}
