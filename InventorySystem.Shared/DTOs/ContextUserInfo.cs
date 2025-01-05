namespace InventorySystem.Shared.DTOs
{
    public class ContextUserInfo
    {
        public required long Id { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public string? TimeZone_Info { get; set; }
    }
}
