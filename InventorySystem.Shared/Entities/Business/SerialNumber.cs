using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Business
{
    public class SerialNumber : BaseEntity
    {
        public required string Serial { get; set; }

        public required long ItemId { get; set; }
        public virtual Item? Item { get; set; }
    }
}
