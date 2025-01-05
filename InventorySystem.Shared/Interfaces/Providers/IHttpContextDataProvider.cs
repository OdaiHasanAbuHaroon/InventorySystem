using InventorySystem.Shared.DTOs;

namespace InventorySystem.Shared.Interfaces.Providers
{
    /// <summary>
    /// Provides methods to access HTTP context-related data such as connection string, user details, and other context-specific information.
    /// </summary>
    public interface IHttpContextDataProvider
    {
        /// <summary>
        /// Retrieves the connection string associated with the current HTTP context.
        /// </summary>
        /// <returns>A string representing the connection string.</returns>
        string GetConnectionString();

        /// <summary>
        /// Retrieves the current user's unique identifier.
        /// </summary>
        /// <returns>A string representing the user's ID.</returns>
        string GetCurrentUserId();

        /// <summary>
        /// Retrieves the current user's email address.
        /// </summary>
        /// <returns>A string representing the user's email.</returns>
        string GetCurrentUserEmail();

        /// <summary>
        /// Retrieves the timezone associated with the current HTTP context.
        /// </summary>
        /// <returns>A string representing the timezone.</returns>
        string GetTimeZone();

        /// <summary>
        /// Retrieves detailed context information about the current user.
        /// </summary>
        /// <returns>A <see cref="ContextUserInfo"/> object containing user context information, or null if unavailable.</returns>
        ContextUserInfo? GetContextUserInfo();

        /// <summary>
        /// Retrieves the user agent string from the current HTTP context.
        /// </summary>
        /// <returns>A string representing the user agent.</returns>
        string GetUserAgent();
    }
}
