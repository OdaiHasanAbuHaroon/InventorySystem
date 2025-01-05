using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Api.ApiBaseServices
{
    /// <summary>
    /// Provides utility methods for creating standardized responses and sending emails in the application.
    /// </summary>
    public class ApiHelperService : IHelperService
    {
        private readonly ICustomMessageProvider _messageProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiHelperService> _logger;
        private readonly ISmtpService _smtpService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly bool _detailedError = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHelperService"/> class.
        /// </summary>
        /// <param name="customMessageProvider">Service for retrieving localized messages.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <param name="logger">Logging service for error and activity logging.</param>
        /// <param name="smtpService">Service for sending emails.</param>
        /// <param name="httpContextAccessor">Accessor for retrieving HTTP context details.</param>
        public ApiHelperService(ICustomMessageProvider customMessageProvider, IConfiguration configuration, ILogger<ApiHelperService> logger, ISmtpService smtpService, IHttpContextAccessor httpContextAccessor)
        {
            _messageProvider = customMessageProvider;
            _configuration = configuration;
            _logger = logger;
            _smtpService = smtpService;
            _httpContextAccessor = httpContextAccessor;
            _detailedError = _configuration.GetValue<bool>("DetailedError", false);
        }

        /// <summary>
        /// Creates a standardized error response with a single message.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="messageKey">Key for the localized error message.</param>
        /// <param name="lang">Language code for the message.</param>
        /// <param name="exp">Optional exception for detailed error messages.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> with error details.</returns>
        public GenericResponse<T> CreateErrorResponse<T>(string messageKey, string lang, Exception? exp = null)
        {
            var message = _detailedError && exp != null
                ? exp.Message
                : _messageProvider.Find(messageKey, lang);

            return new GenericResponse<T>
            {
                Success = false,
                Response = default,
                Messages =
                        [
                            new ResponseMessage
                            {
                                Message = message,
                                Type = ResponseMessageType.Error,
                                Code = ResponseMessageCode.ErrorStatusCode
                            }
                        ]
            };
        }

        /// <summary>
        /// Creates an error response using a list of response messages.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="listMessage">List of response messages.</param>
        /// <param name="lang">Language code for the messages.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> with error details.</returns>
        public GenericResponse<T> CreateErrorResponse<T>(List<ResponseMessage> listMessage, string lang)
        {
            foreach (var message in listMessage)
            {
                if (message != null && !string.IsNullOrEmpty(message.Message))
                {
                    message.Message = _messageProvider.Find(message.Message, lang);
                    message.Type = ResponseMessageType.Error;
                    message.Code = ResponseMessageCode.ErrorStatusCode;
                }
            }
            return new GenericResponse<T>
            {
                Success = false,
                Response = default,
                Messages = listMessage
            };
        }

        /// <summary>
        /// Creates an error response using a list of message keys.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="listMessage">List of message keys.</param>
        /// <param name="lang">Language code for the messages.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> with error details.</returns>
        public GenericResponse<T> CreateErrorResponse<T>(List<string> listMessage, string lang)
        {
            List<ResponseMessage> listResponse = [];
            foreach (var message in listMessage)
            {
                listResponse.Add(new ResponseMessage() { Code = ResponseMessageCode.ErrorStatusCode, Message = _messageProvider.Find(message, lang), Type = ResponseMessageType.Error });
            }
            return new GenericResponse<T>
            {
                Success = false,
                Response = default,
                Messages = listResponse
            };
        }

        /// <summary>
        /// Creates a standardized warning response with a single warning message.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="messageKey">The key for the localized warning message.</param>
        /// <param name="lang">The language code for the message.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object containing the warning details and no response data.
        /// </returns>
        public GenericResponse<T> CreateWarningResponse<T>(string messageKey, string lang)
        {
            return new GenericResponse<T>
            {
                Success = false,
                Response = default,
                Messages = new List<ResponseMessage>() { new() { Message = _messageProvider.Find(messageKey, lang), Type = ResponseMessageType.Warning, Code = ResponseMessageCode.WarningStatusCode } }
            };
        }

        /// <summary>
        /// Creates a standardized warning response with a list of warning messages.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="listMessage">A list of <see cref="ResponseMessage"/> objects containing the warning messages.</param>
        /// <param name="lang">The language code for localizing the messages.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object containing the warning details and no response data.
        /// </returns>
        public GenericResponse<T> CreateWarningResponse<T>(List<ResponseMessage> listMessage, string lang)
        {
            foreach (var message in listMessage)
            {
                if (message != null && !string.IsNullOrEmpty(message.Message))
                {
                    message.Message = _messageProvider.Find(message.Message, lang);
                    message.Type = ResponseMessageType.Warning;
                    message.Code = ResponseMessageCode.WarningStatusCode;
                }
            }
            return new GenericResponse<T>
            {
                Success = false,
                Response = default,
                Messages = listMessage
            };
        }

        /// <summary>
        /// Creates a standardized success response with a localized success message and additional metadata.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="messageKey">The key for the localized success message.</param>
        /// <param name="lang">The language code for localizing the message.</param>
        /// <param name="data">The response data to be included in the success response.</param>
        /// <param name="totalRecords">The total number of records (optional, for paginated responses).</param>
        /// <param name="page">The current page number (optional, for paginated responses).</param>
        /// <param name="pageSize">The size of the page (optional, for paginated responses).</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object containing the success details, response data, and additional metadata.
        /// </returns>
        public GenericResponse<T> CreateSuccessResponse<T>(string messageKey, string lang, T data, long? totalRecords = null, long? page = null, long? pageSize = null)
        {
            return new GenericResponse<T>
            {
                Success = true,
                Response = data,
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                Messages = [new ResponseMessage() { Message = _messageProvider.Find(messageKey, lang), Type = ResponseMessageType.Success, Code = ResponseMessageCode.SuccessStatusCode.ToString() }]
            };
        }

        /// <summary>
        /// Creates a standardized success response with response data and optional pagination metadata.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">The main response data to be included in the response.</param>
        /// <param name="totalRecords">The total number of records (optional, for paginated responses).</param>
        /// <param name="page">The current page number (optional, for paginated responses).</param>
        /// <param name="pageSize">The size of the page (optional, for paginated responses).</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object indicating success and containing the response data and optional pagination metadata.
        /// </returns>
        public GenericResponse<T> CreateSuccessResponse<T>(T data, long? totalRecords = null, long? page = null, long? pageSize = null)
        {
            return new GenericResponse<T>
            {
                Success = true,
                Response = data,
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Creates a standardized success response with response data and optional pagination details.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">The main response data to be included in the response.</param>
        /// <param name="page">The current page number (optional, for paginated responses).</param>
        /// <param name="pageSize">The size of the page (optional, for paginated responses).</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object indicating success and containing the response data and optional pagination details.
        /// </returns>
        public GenericResponse<T> CreateSuccessResponse<T>(T data, long? page = null, long? pageSize = null)
        {
            return new GenericResponse<T>
            {
                Success = true,
                Response = data,
                Page = page,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// Creates a standardized success response with response data.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="data">The main response data to be included in the response.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> object indicating success and containing the response data.
        /// </returns>
        public GenericResponse<T> CreateSuccessResponse<T>(T data)
        {
            return new GenericResponse<T>
            {
                Success = true,
                Response = data,
            };
        }
    }
}
