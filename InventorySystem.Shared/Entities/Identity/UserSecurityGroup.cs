using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class UserSecurityGroup : MinEntity
    {
        public required long SecurityGroupId { get; set; }
        public virtual SecurityGroup? SecurityGroup { get; set; }

        public required long UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
