namespace InventorySystem.Api.Middlewares
{
    /// <summary>
    /// Middleware to handle uncaught exceptions during request processing and ensure proper logging of errors.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        /// <summary>
        /// Initializes the middleware with the next delegate in the pipeline and a logger for error tracking.
        /// </summary>
        /// <param name="next">Delegate to invoke the next middleware in the pipeline.</param>
        /// <param name="logger">Logger to record error details.</param>
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _logger.LogInformation("Errors Tracker Started");
        }

        /// <summary>
        /// Captures and handles uncaught exceptions in the HTTP request pipeline.
        /// </summary>
        /// <param name="context">HTTP context of the current request.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Errors-Tracker applied *");
                await _next(context); // Proceed with the next middleware
            }
            catch (Exception error)
            {
                // Log the exception details
                _logger.LogError(error, error.Message, error.StackTrace);

                // Check if the response has already started
                if (!context.Response.HasStarted)
                {
                    // Handle the error by setting a status code and writing to the response
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("An error occurred.");
                }
                else
                {
                    // Response already started, log and avoid modifying the response
                    _logger.LogWarning("The response has already started, cannot modify the response.");
                }
            }
        }
    }
}
