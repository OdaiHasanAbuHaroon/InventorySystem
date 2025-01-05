using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InventorySystem.Api.Filters
{
    /// <summary>
    /// Adds custom headers to Swagger UI for all API operations.
    /// </summary>
    public class CustomHeadersOperationFilter : IOperationFilter
    {
        private readonly string _AppId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeadersOperationFilter"/> class.
        /// </summary>
        /// <param name="key">The application ID to be included as a default value in the `appid` header.</param>
        public CustomHeadersOperationFilter(string key)
        {
            _AppId = key;
        }

        /// <summary>
        /// Adds custom headers (`lang`, `timezone`, `appid`) to the API operations in Swagger.
        /// </summary>
        /// <param name="operation">The current API operation being processed.</param>
        /// <param name="context">The filter context for the current operation.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add 'lang' header
            string localTimeZone = TimeZoneInfo.Local.Id;
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "lang",
                In = ParameterLocation.Header,
                Description = "Language code for localization (e.g., en, es, fr)",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("en") // Default to "en"
                }
            });

            // Add 'Timezone' header
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "timezone",
                In = ParameterLocation.Header,
                Description = "Timezone of the client (e.g., Arab Standard Time)",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(localTimeZone) // Set the default as UTC, or replace dynamically
                }
            });

            // Add 'AppId' header
            if (!string.IsNullOrEmpty(_AppId))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "appid",
                    In = ParameterLocation.Header,
                    Description = "Client  App ID of the client (GUID)",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString(_AppId)
                    }
                });
            }
        }
    }
}
