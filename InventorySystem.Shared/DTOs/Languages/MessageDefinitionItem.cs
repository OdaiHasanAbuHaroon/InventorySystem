namespace InventorySystem.Shared.DTOs.Languages
{
    public class MessageDefinitionItem
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Message { get; set; }

        public required string ServiceName { get; set; }

        public Dictionary<string, string>? Translations { get; set; }
    }
}
