using System.Globalization;
using System.Text.Json;

namespace InventorySystem.Api.Middlewares
{
    /// <summary>
    /// Middleware to convert all DateTime properties in incoming JSON requests from the user's timezone to UTC.
    /// </summary>
    public class DateTimeConversionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DateTimeConversionMiddleware> _logger;

        /// <summary>
        /// Initializes the middleware with the next delegate in the pipeline and a logger for tracking DateTime conversions.
        /// </summary>
        /// <param name="next">Delegate to invoke the next middleware in the pipeline.</param>
        /// <param name="logger">Logger for recording conversion activity and errors.</param>
        public DateTimeConversionMiddleware(RequestDelegate next, ILogger<DateTimeConversionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _logger.LogInformation("DateTimeConversion Tracker Started");
        }

        /// <summary>
        /// Intercepts HTTP requests, checks for a 'Timezone' header, and converts all DateTime properties in the JSON body from the user's timezone to UTC.
        /// </summary>
        /// <param name="context">HTTP context of the current request.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Check if the request contains a body and is JSON content
                if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
                {
                    // Check if the 'Timezone' header is present
                    if (context.Request.Headers.TryGetValue("timezone", out var timezoneHeader))
                    {
                        var timeZoneId = timezoneHeader.FirstOrDefault();
                        if (!string.IsNullOrEmpty(timeZoneId))
                        {
                            try
                            {
                                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

                                // Enable buffering to allow reading the request body multiple times
                                context.Request.EnableBuffering();

                                // Read and process the request body
                                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                                if (!string.IsNullOrWhiteSpace(requestBody))
                                {
                                    // Convert DateTime properties from user's timezone to UTC
                                    var modifiedRequestBody = ConvertDateTimesToUtc(requestBody, timeZone);

                                    // Replace the request body with the modified version
                                    var modifiedRequestStream = new MemoryStream();
                                    var writer = new StreamWriter(modifiedRequestStream);
                                    writer.Write(modifiedRequestBody);
                                    writer.Flush();
                                    modifiedRequestStream.Position = 0;

                                    // Set the modified request body to the HttpContext
                                    context.Request.Body = modifiedRequestStream;
                                }
                                context.Request.Body.Position = 0;
                            }
                            catch (TimeZoneNotFoundException exception)
                            {
                                _logger.LogWarning("Invalid timezone ID provided: {TimeZoneId}", timeZoneId + ", exception message: " + exception.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.StackTrace);
            }
            finally
            {
                _logger.LogInformation("DateTime-Conversion applied *");
                // Call the next middleware
                await _next(context);
            }
        }

        /// <summary>
        /// Converts all DateTime properties in the given JSON string from the user's timezone to UTC.
        /// </summary>
        /// <param name="json">The JSON string containing DateTime properties.</param>
        /// <param name="timeZone">The user's timezone.</param>
        /// <returns>A JSON string with all DateTime properties converted to UTC.</returns>
        private string ConvertDateTimesToUtc(string json, TimeZoneInfo timeZone)
        {
            var jsonDoc = JsonDocument.Parse(json);

            using (var memoryStream = new MemoryStream())
            using (var writer = new Utf8JsonWriter(memoryStream))
            {
                ConvertDateTimesRecursive(jsonDoc.RootElement, writer, timeZone);
                writer.Flush();
                return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Recursively processes a JSON document to convert all DateTime properties to UTC.
        /// </summary>
        /// <param name="element">The JSON element to process.</param>
        /// <param name="writer">The JSON writer to write the modified elements.</param>
        /// <param name="timeZone">The user's timezone.</param>
        private void ConvertDateTimesRecursive(JsonElement element, Utf8JsonWriter writer, TimeZoneInfo timeZone)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    writer.WriteStartObject();
                    foreach (var property in element.EnumerateObject())
                    {
                        writer.WritePropertyName(property.Name);

                        if (property.Value.ValueKind == JsonValueKind.String &&
                            DateTime.TryParse(property.Value.GetString(), out var dateTimeValue))
                        {
                            // Convert the DateTime value to UTC
                            var userDateTime = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Unspecified);
                            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(userDateTime, timeZone);
                            writer.WriteStringValue(utcDateTime.ToString("o", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            ConvertDateTimesRecursive(property.Value, writer, timeZone);
                        }
                    }
                    writer.WriteEndObject();
                    break;

                case JsonValueKind.Array:
                    writer.WriteStartArray();
                    foreach (var arrayElement in element.EnumerateArray())
                    {
                        ConvertDateTimesRecursive(arrayElement, writer, timeZone);
                    }
                    writer.WriteEndArray();
                    break;

                default:
                    element.WriteTo(writer);
                    break;
            }
        }
    }
}
