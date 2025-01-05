using Newtonsoft.Json;

namespace InventorySystem.Api.Middlewares
{
    /// <summary>
    /// Middleware to log incoming HTTP request details into a JSON file for monitoring and debugging purposes.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;
        private readonly bool _collectRequestInfo;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private static readonly object _fileLock = new();

        /// <summary>
        /// Initializes the middleware with configuration settings for logging requests.
        /// </summary>
        /// <param name="next">Delegate to invoke the next middleware in the pipeline.</param>
        /// <param name="configuration">Application configuration for logging settings.</param>
        /// <param name="logger">Logger to log request processing activity and errors.</param>
        public RequestLoggingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logFilePath = configuration["RequestLogFilePath"] ?? "C:/Logs/InventoryDemoApi/RequestLogs.json";
            _collectRequestInfo = Convert.ToBoolean(configuration["CollectRequestInfo"] ?? "false");
            _logger = logger;
            _logger.LogInformation("Request Logging Tracker Started");
        }

        /// <summary>
        /// Intercepts and logs HTTP request details to a log file if enabled.
        /// </summary>
        /// <param name="context">HTTP context of the current request.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (_collectRequestInfo)
                {
                    context.Request.EnableBuffering(); // Enable buffering for the request body
                    var requestBodyContent = await ReadRequestBodyAsync(context.Request);
                    var formData = await ReadFormDataAsync(context.Request);

                    var requestInfo = new
                    {
                        Method = context.Request.Method,
                        Path = context.Request.Path,
                        QueryString = context.Request.QueryString.ToString(),
                        Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                        Body = requestBodyContent,
                        Form = formData,
                        Timestamp = DateTime.UtcNow
                    };

                    LogToFileAsync(JsonConvert.SerializeObject(requestInfo));

                    context.Request.Body.Position = 0; // Rewind the stream for the next middleware or endpoint
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.StackTrace);
            }
            finally
            {
                _logger.LogInformation("Request-Logging applied *");
                await _next(context); // Pass control to the next middleware
            }
        }

        /// <summary>
        /// Reads the body content of the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request to read.</param>
        /// <returns>A string containing the request body.</returns>
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Position = 0; // Ensure the body can be read from the beginning
            using var reader = new StreamReader(request.Body, leaveOpen: true);

            var content = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Rewind the stream after reading

            return content;
        }

        /// <summary>
        /// Reads form data from the HTTP request if the content type is a form.
        /// </summary>
        /// <param name="request">The HTTP request to read form data from.</param>
        /// <returns>A dictionary containing form data or null if no form content type is present.</returns>
        private async Task<Dictionary<string, string>?> ReadFormDataAsync(HttpRequest request)
        {
            if (!request.HasFormContentType)
            {
                return null;
            }

            var formData = new Dictionary<string, string>();
            foreach (var key in request.Form.Keys)
            {
                formData[key] = request.Form[key]!;
            }

            return await Task.FromResult(formData);
        }

        /// <summary>
        /// Logs the serialized request details to a file in a thread-safe manner.
        /// </summary>
        /// <param name="message">The serialized request information to log.</param>
        private void LogToFileAsync(string message)
        {
            try
            {
                // Ensure that file writing is thread-safe
                lock (_fileLock)
                {
                    using var writer = new StreamWriter(_logFilePath, true);
                    writer.WriteLine(message + ",");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to write request log to file.");
            }
        }
    }
}
