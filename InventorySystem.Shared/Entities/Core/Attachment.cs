using InventorySystem.Shared.Entities.BaseEntities;

namespace InventorySystem.Shared.Entities.Core
{
    public class Attachment : BaseEntity
    {
        public required string Name { get; set; }

        public required string Extention { get; set; }

        public required string Path { get; set; }

        #region Collections

        public virtual ICollection<AttachmentBackup> AttachmentBackups { get; set; } = new HashSet<AttachmentBackup>();

        #endregion
    }
}