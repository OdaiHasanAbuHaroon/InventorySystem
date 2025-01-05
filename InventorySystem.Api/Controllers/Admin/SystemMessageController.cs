using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs.Languages;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Admin
{
    /// <summary>
    /// Controller for managing system messages. 
    /// Provides functionality to retrieve and add message definitions used across the application.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemMessageController : ControllerBaseExt
    {
        private readonly ILogger<SystemMessageController> _logService;
        private readonly ICustomMessageProvider _messageProvider;
        private readonly IHelperService _helperService;
        private readonly IAssetsService _assetsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMessageController"/> class.
        /// </summary>
        /// <param name="logsService">Logger for tracking operations and errors.</param>
        /// <param name="messageProvider">Service for handling custom message definitions.</param>
        /// <param name="helperService">Helper service for creating responses and utility functions.</param>
        /// <param name="assetsService">Service for managing assets related to the application.</param>
        public SystemMessageController(
            ILogger<SystemMessageController> logsService,
            ICustomMessageProvider messageProvider,
            IHelperService helperService,
            IAssetsService assetsService)
        {
            _logService = logsService;
            _messageProvider = messageProvider;
            _helperService = helperService;
            _assetsService = assetsService;
        }

        /// <summary>
        /// Retrieves all system message definitions.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="MessageDefinitionItem"/>.
        /// </returns>
        [AllowAnonymous, HttpGet("GetMessageDefinitions")]
        public async Task<GenericResponse<List<MessageDefinitionItem>>> GetMessageDefinitions()
        {
            try
            {
                var messageDefinitions = await _messageProvider.LoadContent();
                return _helperService.CreateSuccessResponse(
                    MessageKeys.info_get_success,
                    GetLanguage(),
                    messageDefinitions
                );
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "Error occurred while retrieving message definitions.");
                return _helperService.CreateErrorResponse<List<MessageDefinitionItem>>(
                    MessageKeys.system_error,
                    GetLanguage()
                );
            }
        }

        /// <summary>
        /// Adds a new system message definition.
        /// </summary>
        /// <param name="definitionForm">The form containing the new message definition details.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the added <see cref="MessageDefinitionItem"/>.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator},{RoleDefinitions.Administrator}"), HttpPost("AddMessageDefinition")]
        public async Task<GenericResponse<MessageDefinitionItem>> AddMessageDefinition(MessageDefinitionForm definitionForm)
        {
            try
            {
                var result = await _messageProvider.AddMessageDefinition(definitionForm);
                if (result != null)
                {
                    return _helperService.CreateSuccessResponse(
                        MessageKeys.info_get_success,
                        GetLanguage(),
                        result
                    );
                }
                else
                {
                    return _helperService.CreateErrorResponse<MessageDefinitionItem>(
                        MessageKeys.system_error,
                        GetLanguage()
                    );
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "Error occurred while adding a message definition.");
                return _helperService.CreateErrorResponse<MessageDefinitionItem>(
                    MessageKeys.system_error,
                    GetLanguage()
                );
            }
        }
    }
}
