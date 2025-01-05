using Google.Cloud.Translation.V2;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using Newtonsoft.Json;

namespace InventorySystem.Api.ApiBaseServices
{
    /// <summary>
    /// Service for translating text using Google Cloud Translation API.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TranslationService> _logService;
        private readonly string? _googleKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for retrieving settings.</param>
        /// <param name="logService">The logging service for capturing logs.</param>
        public TranslationService(IConfiguration configuration, ILogger<TranslationService> logService)
        {
            _configuration = configuration;
            _logService = logService;
            _googleKey = _configuration.GetValue<string>("googlcouldapikey");
        }

        /// <summary>
        /// Translates the provided text to the target language using Google Cloud Translation API.
        /// </summary>
        /// <param name="source">The text to translate.</param>
        /// <param name="TargetLang">The language code to translate the text into.</param>
        /// <returns>
        /// The translated text if successful, or <c>null</c> if an error occurs or the input is invalid.
        /// </returns>
        private async Task<string?> CloudTranslation(string source, string TargetLang)
        {
            try
            {
                if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(_googleKey))
                {
                    var client = TranslationClient.CreateFromApiKey(_googleKey);
                    var response = await client.TranslateTextAsync(source, TargetLang);
                    return response.TranslatedText;
                }

                return null;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// Translates a single piece of text to the specified language asynchronously.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <param name="lang">The target language code (default is "en").</param>
        /// <returns>
        /// The translated text if successful, or <c>null</c> if an error occurs.
        /// </returns>
        public async Task<string?> TranslateTextAsync(string text, string lang = "en")
        {
            return await CloudTranslation(text, lang);
        }

        /// <summary>
        /// Translates the provided text into all supported languages and returns the translations as a JSON string.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <returns>
        /// A JSON string containing translations for all supported languages, or <c>null</c> if no translations are successful.
        /// </returns>
        public async Task<string?> GetTranslationsAsync(string text)
        {
            var translations = new Dictionary<string, string>();
            foreach (var code in LanguageCodeDefinitions.codes)
            {
                var translation = await TranslateTextAsync(text, code);
                if (translation != null)
                {
                    translations.Add(code, translation);
                }
            }

            return translations.Count > 0 ? JsonConvert.SerializeObject(translations) : null;
        }
    }
}
