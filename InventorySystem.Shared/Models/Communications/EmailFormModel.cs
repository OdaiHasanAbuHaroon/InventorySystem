namespace InventorySystem.Shared.Models.Communications
{
    public class EmailFormModel
    {
        public string? Application { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

        public string? From { get; set; }

        public string? To { get; set; }

        public string? Cc { get; set; }

        public string? Bcc { get; set; }

        public bool IsSent { get; set; }

        public string? Attachments { get; set; }

        public string? EmailType { get; set; }

        public DateTime? SendDate { get; set; }
    }
}
