using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Extentions
{
    /// <summary>
    /// Extended ControllerBase class to provide utility methods for fetching request headers and user-related information.
    /// </summary>
    public class ControllerBaseExt : ControllerBase
    {
        /// <summary>
        /// Fetches the language code from the "lang" header in the request.
        /// </summary>
        /// <returns>
        /// A string representing the language code. 
        /// Returns the default value "en" if the "lang" header is not present or invalid.
        /// </returns>
        protected string GetLanguage()
        {
            if (Request.Headers.TryGetValue("lang", out var lang))
            {
                string? result = lang.FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }
            return "en";
        }

        /// <summary>
        /// Fetches the timezone from the "timezone" header in the request.
        /// </summary>
        /// <returns>
        /// A string representing the timezone.
        /// Returns the default value "UTC" if the "timezone" header is not present or invalid.
        /// </returns>
        protected string GetTimeZone()
        {
            if (Request.Headers.TryGetValue("timezone", out var timeZone))
            {
                string? result = timeZone.FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }
            return "UTC";
        }

        /// <summary>
        /// Retrieves the ID of the currently authenticated user from the claims.
        /// </summary>
        /// <returns>
        /// A string representing the current authenticated user ID.
        /// Returns "Anonymous" if the user is not authenticated or the claim is missing.
        /// </returns>
        protected string CurrentUserId()
        {
            if (User != null)
            {
                if (User.Identity != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                        if (UserId != null)
                        {
                            return UserId.Value;
                        }
                    }
                }
            }
            return "Anonymous";
        }
    }
}
