namespace InventorySystem.Shared.Entities.Core
{
    public class AttachmentBackup
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public required string Extention { get; set; }

        public required string Base64Content { get; set; }

        public required long AttachmentId { get; set; }
        public virtual Attachment? Attachment { get; set; }
    }
}
