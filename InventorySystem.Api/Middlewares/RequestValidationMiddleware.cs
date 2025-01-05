namespace InventorySystem.Api.Middlewares
{
    /// <summary>
    /// Middleware to validate incoming requests based on allowed hosts, user agents, headers, and AppIds.
    /// </summary>
    public class RequestValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestValidationMiddleware> _logger;
        private readonly List<string> _allowedHosts;
        private readonly List<string> _allowedUserAgents;
        private readonly bool _allowAllHosts;
        private readonly string AppIds;

        /// <summary>
        /// Initializes the middleware with necessary configurations and logger.
        /// </summary>
        /// <param name="next">Delegate to invoke the next middleware in the pipeline.</param>
        /// <param name="configuration">Application configuration for retrieving validation settings.</param>
        /// <param name="logger">Logger to log validation activity and errors.</param>
        public RequestValidationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<RequestValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            var allowedHostsConfig = configuration["AllowedHosts"];
            _allowAllHosts = allowedHostsConfig == "*";
            _allowedHosts = !_allowAllHosts ? allowedHostsConfig?.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>() : new List<string>();

            var allowedUserAgentsConfig = configuration["AllowedUserAgents"];
            _allowedUserAgents = allowedUserAgentsConfig?.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
            AppIds = configuration["AppIds"] ?? "";
            _logger.LogInformation("Request Validation Tracker Started");
        }

        /// <summary>
        /// Validates incoming requests based on referer, host, user agent, headers, and AppIds.
        /// </summary>
        /// <param name="context">HTTP context of the current request.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var referer = context.Request.Headers["Referer"].FirstOrDefault();
                var host = context.Request.Headers["Host"].FirstOrDefault();
                var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();
                var requestOrigin = referer ?? host;

                // Validate the request origin against the allowed hosts
                if (!_allowAllHosts && !string.IsNullOrEmpty(requestOrigin) && !_allowedHosts.Any(h => requestOrigin.Contains(h, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("Request from disallowed origin: {Origin}", requestOrigin);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Request origin not allowed.");
                    return; // Ensure no further processing
                }

                // Validate the request user agent against the allowed user agents
                if (string.IsNullOrEmpty(userAgent) || !_allowedUserAgents.Any(ua => userAgent.Contains(ua, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("Request from disallowed user agent: {UserAgent}", userAgent);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("User agent not allowed.");
                    return; // Ensure no further processing
                }

                // Allow requests to Swagger UI to proceed without 'timezone' header
                if (context.Request.Path.HasValue && !context.Request.Path.Value.Contains("/api/") && (context.Request.Path.Value.Contains("/swagger") || (context.Request.Path.Value.Contains("/Images"))))
                {
                    await _next(context);  // Allow the request to proceed
                    return;  // Stop further execution for swagger paths
                }

                // Validate the presence of the 'timezone' header
                if (!context.Request.Headers.ContainsKey("timezone"))
                {
                    _logger.LogWarning("Request missing 'Timezone' header.");
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Request missing required 'Timezone' header.");
                    return; // Ensure no further processing
                }

                // Validate the presence of the 'appid' header
                if (!context.Request.Headers.ContainsKey("appid"))
                {
                    _logger.LogWarning("Request missing 'AppId' header.");
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Request missing required 'AppId' header.");
                    return; // Ensure no further processing
                }

                // Check the 'appid' value against allowed AppIds
                var appId = context.Request.Headers["appid"].FirstOrDefault();
                if (appId == null || !AppIds.Contains(appId))
                {
                    _logger.LogWarning("Invalid 'AppId' header: {AppId}", appId);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Invalid AppId.");
                    return; // Ensure no further processing
                }

                // If validation passes, proceed to the next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.StackTrace);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred while processing the request.");
            }
        }
    }
}
