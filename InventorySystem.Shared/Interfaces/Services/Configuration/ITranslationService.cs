namespace InventorySystem.Shared.Interfaces.Services.Configuration
{
    public interface ITranslationService
    {
        Task<string?> TranslateTextAsync(string text, string lang = "en");

        public Task<string?> GetTranslationsAsync(string text);
    }
}
