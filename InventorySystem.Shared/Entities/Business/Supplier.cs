using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Core;

namespace InventorySystem.Shared.Entities.Business
{
    public class Supplier : BaseEntity
    {
        public required string Name { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactName { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public long? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
