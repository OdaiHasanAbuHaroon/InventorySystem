using InventorySystem.Shared.DTOs.Languages;

namespace InventorySystem.Shared.Interfaces.Services.Configuration
{
    public interface ICustomMessageProvider
    {
        string Find(string name, string lang);

        Task<List<MessageDefinitionItem>> LoadContent();

        Task<MessageDefinitionItem?> AddMessageDefinition(MessageDefinitionForm definitionForm);
    }
}
