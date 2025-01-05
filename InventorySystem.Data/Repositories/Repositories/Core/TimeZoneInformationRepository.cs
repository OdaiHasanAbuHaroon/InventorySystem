using InventorySystem.Shared.Entities.Core;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Core;

namespace InventorySystem.Data.Repositories.Repositories.Core
{
    public class TimeZoneInformationRepository : GenericRepository<TimeZoneInformation>, ITimeZoneInformationRepository
    {
        public TimeZoneInformationRepository(DatabaseContext context) : base(context) { }
    }
}