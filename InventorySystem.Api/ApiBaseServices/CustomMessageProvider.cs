using System.Timers;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs.Languages;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using Newtonsoft.Json;

namespace InventorySystem.Api.ApiBaseServices
{
    /// <summary>
    /// Provides custom message definitions with support for translation and periodic content updates.
    /// </summary>
    public class CustomMessageProvider : ICustomMessageProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomMessageProvider> _logService;
        private readonly ITranslationService _translationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly System.Timers.Timer _timerContentUpdate;
        private readonly System.Timers.Timer _timerTranslationUpdate;
        private List<MessageDefinitionItem> messageDefinitionItems = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMessageProvider"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration for retrieving settings.</param>
        /// <param name="logService">The logging service for error and activity logging.</param>
        /// <param name="translationService">The service for translating message definitions.</param>
        /// <param name="webHost">The web host environment for accessing content paths.</param>
        public CustomMessageProvider(IConfiguration configuration, ILogger<CustomMessageProvider> logService, ITranslationService translationService, IWebHostEnvironment webHost)
        {
            _configuration = configuration;
            _translationService = translationService;
            _logService = logService;
            _webHostEnvironment = webHost;
            var def = _configuration.GetSection("MessageDefinitions").Get<List<MessageDefinitionItem>>();
            if (def != null) { messageDefinitionItems = def; }
            _timerContentUpdate = new System.Timers.Timer(TimeSpan.FromMinutes(1))
            {
                AutoReset = true,
                Enabled = true
            };
            _timerContentUpdate.Elapsed += TimerContentUpdate_Elapsed;
            _timerContentUpdate.Start();
            _timerTranslationUpdate = new System.Timers.Timer(TimeSpan.FromMinutes(1))
            {
                AutoReset = true,
                Enabled = true
            };
            _timerTranslationUpdate.Elapsed += TimerTranslationUpdate_Elapsed; ;
            _timerTranslationUpdate.Start();
        }

        /// <summary>
        /// Periodically updates translations for message definitions.
        /// </summary>
        private async void TimerTranslationUpdate_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await RunTranslation();
        }

        /// <summary>
        /// Updates translations for all message definitions, adding translations for missing languages.
        /// </summary>
        private async Task RunTranslation()
        {
            try
            {
                _timerContentUpdate.Stop();
                _timerTranslationUpdate.Stop();
                var def = _configuration.GetSection("MessageDefinitions").Get<List<MessageDefinitionItem>>();
                if (def != null)
                {
                    if (def.Count > 0)
                    {
                        foreach (var MessageDef in def)
                        {
                            MessageDef.Translations ??= new Dictionary<string, string>();

                            foreach (var lang in LanguageCodeDefinitions.codes)
                            {
                                if (!MessageDef.Translations.ContainsKey(lang))
                                {
                                    var messageTrans = await _translationService.TranslateTextAsync(MessageDef.Message, lang);
                                    if (messageTrans != null)
                                        MessageDef.Translations.Add(lang, messageTrans);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(MessageDef.Translations[lang]))
                                    {
                                        var messageTrans = await _translationService.TranslateTextAsync(MessageDef.Message, lang);
                                        if (messageTrans != null)
                                            MessageDef.Translations.Add(lang, messageTrans);
                                    }
                                }
                            }
                        }
                        var translationUpdate = JsonConvert.SerializeObject(def, Formatting.Indented);
                        await File.WriteAllTextAsync(Path.Combine(_webHostEnvironment.ContentRootPath, "Messages.json"), "{" + $"\"MessageDefinitions\":{translationUpdate}" + "}");
                    }
                }

            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
            finally
            {
                _timerContentUpdate.Start();
                _timerTranslationUpdate.Start();
            }
        }

        /// <summary>
        /// Periodically reloads content for message definitions.
        /// </summary>
        private async void TimerContentUpdate_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await LoadContent();
        }

        /// <summary>
        /// Reloads the message definitions from the configuration.
        /// </summary>
        /// <returns>A list of updated <see cref="MessageDefinitionItem"/> objects.</returns>
        public async Task<List<MessageDefinitionItem>> LoadContent()
        {
            try
            {
                _timerContentUpdate.Stop();
                await Task.Delay(100);
                var def = _configuration.GetSection("MessageDefinitions").Get<List<MessageDefinitionItem>>();
                if (def != null) { messageDefinitionItems = def; }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
            finally
            {
                _timerContentUpdate.Start();
            }
            return messageDefinitionItems;
        }

        /// <summary>
        /// Finds a specific message translation by name and language.
        /// </summary>
        /// <param name="name">The name of the message to find.</param>
        /// <param name="lang">The language code for the translation.</param>
        /// <returns>The translated message if found, or "*" if not found.</returns>
        public string Find(string name, string lang)
        {
            if (messageDefinitionItems != null)
            {
                var search = messageDefinitionItems.Where(x => x.Name == name).FirstOrDefault();
                if (search != null)
                {
                    if (search.Translations != null)
                    {
                        return search.Translations.GetValueOrDefault(lang) ?? "*";
                    }
                }
            }
            return "*";
        }

        /// <summary>
        /// Adds a new message definition to the list and updates translations for all languages.
        /// </summary>
        /// <param name="definitionForm">The form containing the message details.</param>
        /// <returns>
        /// The newly created <see cref="MessageDefinitionItem"/> if successful, or <c>null</c> if an error occurs.
        /// </returns>
        public async Task<MessageDefinitionItem?> AddMessageDefinition(MessageDefinitionForm definitionForm)
        {
            try
            {
                await LoadContent();
                _timerContentUpdate.Stop();
                _timerTranslationUpdate.Stop();
                await Task.Delay(100);
                var msg = messageDefinitionItems.Where(x => x.Name == definitionForm.MessageCode).FirstOrDefault();
                if (msg != null) { return msg; }
                else
                {
                    var NewMessage = new MessageDefinitionItem() { Id = messageDefinitionItems.Count + 1, ServiceName = definitionForm.ServiceName, Message = definitionForm.Message, Name = definitionForm.MessageCode, Translations = new Dictionary<string, string>() };
                    foreach (var lang in LanguageCodeDefinitions.codes)
                    {
                        var messageTrans = await _translationService.TranslateTextAsync(NewMessage.Message, lang);
                        if (messageTrans != null)
                            NewMessage.Translations.Add(lang, messageTrans);
                    }
                    messageDefinitionItems.Add(NewMessage);
                    for (int i = 1; i <= messageDefinitionItems.Count; i++)
                    {
                        messageDefinitionItems[i - 1].Id = i;
                    }
                    var translationUpdate = JsonConvert.SerializeObject(messageDefinitionItems, Formatting.Indented);
                    await File.WriteAllTextAsync(Path.Combine(_webHostEnvironment.ContentRootPath, "Messages.json"), "{" + $"\"MessageDefinitions\":{translationUpdate}" + "}");
                    return NewMessage;
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
            finally
            {
                _timerContentUpdate.Start();
                _timerTranslationUpdate.Start();
            }
            return null;
        }
    }
}
