using InventorySystem.Shared.Entities.Core;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Core;

namespace InventorySystem.Data.Repositories.Repositories.Core
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(DatabaseContext context) : base(context) { }
    }
}