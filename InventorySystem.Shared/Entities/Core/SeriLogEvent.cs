using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Core
{
    public class SeriLogEvent : BaseEntity
    {
        public required string Message { get; set; }

        public required string MessageTemplate { get; set; }

        public required string Level { get; set; }

        public required string Exception { get; set; }

        public required string LogEvent { get; set; }

        public required string Properties { get; set; }

        public required DateTime TimeStamp { get; set; }
    }
}
