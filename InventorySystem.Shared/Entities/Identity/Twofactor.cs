using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Configuration.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Shared.Entities.Configuration
{
    public class Twofactor : BaseEntity
    {
        public required long UserId { get; set; }
        public virtual User? User { get; set; }

        public required string Code { get; set; }

        public required DateTime ExpirationDate { get; set; }

        public required bool IsUsed { get; set; } = false;

        public required bool IsSent { get; set; } = false;

        public required string Stamp { get; set; }

        [Range(1, 2, ErrorMessage = "Value must be between 1 and 2.")]
        public required int RequestType { get; set; }
    }
}
