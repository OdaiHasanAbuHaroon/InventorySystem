using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Responses;
using Newtonsoft.Json;

namespace InventorySystem.Api.Middlewares
{
    /// <summary>
    /// Middleware to handle maintenance mode for the application.
    /// When maintenance mode is enabled, incoming requests will receive a service unavailable response with a warning message.
    /// </summary>
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _maintenanceMode;
        private readonly ICustomMessageProvider _messageProvider;
        private readonly ILogger<MaintenanceMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="configuration">The application configuration containing maintenance mode settings.</param>
        /// <param name="customMessageProvider">Service for retrieving localized messages.</param>
        /// <param name="logger">Logger for tracking maintenance mode activities.</param>
        public MaintenanceMiddleware(RequestDelegate next, IConfiguration configuration, ICustomMessageProvider customMessageProvider, ILogger<MaintenanceMiddleware> logger)
        {
            _next = next;
            _maintenanceMode = Convert.ToBoolean(configuration["MaintenanceMode"] ?? "false");
            _messageProvider = customMessageProvider;
            _logger = logger;
            _logger.LogInformation("Maintenance Mode Tracker Started ");
        }

        /// <summary>
        /// Invokes the middleware logic asynchronously.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (_maintenanceMode)
                {
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

                    var result = new GenericResponse<bool>
                    {
                        Success = false,
                        Messages = new List<ResponseMessage>
                        {
                            new() {
                                Code = ResponseMessageCode.WarningStatusCode,
                                Message = _messageProvider.Find("under_maintenance", GetLanguage(context)),
                                Type = ResponseMessageType.Warning
                            }
                        },
                        Response = false
                    };

                    // Write the response and stop further processing
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.StackTrace);
            }

            // Proceed to the next middleware if not in maintenance mode
            await _next(context);
        }

        /// <summary>
        /// Retrieves the language code from the request headers.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>The language code specified in the request headers, or "en" if none is provided.</returns>
        private string GetLanguage(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("lang", out var lang))
            {
                string? result = lang.FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return "en";
        }
    }
}
