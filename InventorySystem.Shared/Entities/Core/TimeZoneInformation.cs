using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;

namespace InventorySystem.Shared.Entities.Core
{
    public class TimeZoneInformation : BaseEntity
    {
        public required string Value { get; set; }

        public required string DisplayName { get; set; }

        public required string StandardName { get; set; }

        public required string DaylightName { get; set; }

        public required TimeSpan BaseUtcOffset { get; set; }

        public required bool SupportsDaylightSavingTime { get; set; }

        public required int UtcOffset { get; set; }

        #region Collections

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        #endregion
    }
}
