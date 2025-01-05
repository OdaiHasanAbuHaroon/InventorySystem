using System.Security.Claims;
using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.Interfaces.Providers;

namespace InventorySystem.Api.ApiBaseServices
{
    /// <summary>
    /// Provides user session details and contextual information based on the current HTTP request and user claims.
    /// </summary>
    public class UserSessionDetailProvider : IHttpContextDataProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSessionDetailProvider"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The accessor to retrieve the current HTTP context.</param>
        /// <param name="configuration">The configuration instance for accessing application settings.</param>
        public UserSessionDetailProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the connection string for the current user's database, based on their claims.
        /// </summary>
        /// <returns>The connection string if found, or an empty string if not.</returns>
        public string GetConnectionString()
        {

            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    if (user != null)
                    {
                        if (user.Identity != null)
                        {
                            if (user.Identity.IsAuthenticated)
                            {
                                var databasename = user.Claims.Where(x => x.Type == "DatabaseName").FirstOrDefault();
                                if (databasename != null)
                                {
                                    return _configuration.GetConnectionString(databasename.Value) ?? string.Empty;
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves the current user's unique identifier.
        /// </summary>
        /// <returns>The user's ID if authenticated, or "anonymous" if not.</returns>
        public string GetCurrentUserId()
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    if (user != null)
                    {
                        if (user.Identity != null)
                        {
                            if (user.Identity.IsAuthenticated)
                            {
                                var userId = user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                                if (userId != null)
                                {
                                    return userId.Value;
                                }

                            }
                        }
                    }
                }
            }
            return "anonymous";
        }

        /// <summary>
        /// Retrieves the current user's email address.
        /// </summary>
        /// <returns>The user's email if authenticated, or "anonymous" if not.</returns>
        public string GetCurrentUserEmail()
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    if (user != null)
                    {
                        if (user.Identity != null)
                        {
                            if (user.Identity.IsAuthenticated)
                            {
                                var email = user.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
                                if (email != null)
                                {
                                    return email.Value;
                                }
                            }
                        }
                    }
                }
            }
            return "anonymous";
        }

        /// <summary>
        /// Retrieves contextual information about the current user, including ID, email, and timezone.
        /// </summary>
        /// <returns>A <see cref="ContextUserInfo"/> object if authenticated, or null if not.</returns>
        public ContextUserInfo? GetContextUserInfo()
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    if (user != null)
                    {
                        if (user.Identity != null)
                        {
                            if (user.Identity.IsAuthenticated)
                            {
                                try
                                {
                                    var email = user.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
                                    var userId = user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                                    var lastName = user.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();
                                    var TimeZone_Info = user.Claims.Where(x => x.Type == "Timezone").FirstOrDefault();
                                    if (email != null && userId != null && lastName != null)
                                    {
                                        return new ContextUserInfo()
                                        {
                                            Email = email.Value,
                                            Id = long.Parse(userId.Value),
                                            LastName = lastName.Value,
                                            TimeZone_Info = TimeZone_Info != null ? TimeZone_Info.Value : "UTC"
                                        };
                                    }
                                }
                                catch
                                {
                                    throw new Exception("Invalid User Claims on :UserSessionDetailProvider");
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the timezone of the current user based on the request headers or claims.
        /// </summary>
        /// <returns>The timezone if found, or "UTC" if not.</returns>
        public string GetTimeZone()
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    if (_httpContextAccessor.HttpContext.Request != null)
                    {
                        if (_httpContextAccessor.HttpContext.Request.Headers != null)
                        {
                            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("timezone", out var Timezone);
                            string? result = Timezone.FirstOrDefault();
                            if (!string.IsNullOrEmpty(result))
                            {
                                return result;
                            }
                        }
                    }

                    var user = _httpContextAccessor.HttpContext.User;
                    if (user != null)
                    {
                        if (user.Identity != null)
                        {
                            if (user.Identity.IsAuthenticated)
                            {
                                try
                                {
                                    var TimeZone_InfoId = user.Claims.Where(x => x.Type == "Timezone").FirstOrDefault();
                                    return TimeZone_InfoId != null ? TimeZone_InfoId.Value : "UTC";
                                }
                                catch
                                {
                                    return "UTC";
                                }
                            }
                        }
                    }
                }
            }
            return "UTC";
        }

        /// <summary>
        /// Retrieves the User-Agent of the current HTTP request.
        /// </summary>
        /// <returns>The User-Agent if found, or "anonymous" if not.</returns>
        public string GetUserAgent()
        {
            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    if (_httpContextAccessor.HttpContext.Request != null)
                    {
                        if (_httpContextAccessor.HttpContext.Request.Headers != null)
                        {
                            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("User-Agent", out var useragent);
                            string? result = useragent.FirstOrDefault();
                            if (!string.IsNullOrEmpty(result))
                            {
                                return result;
                            }
                        }
                    }
                }
            }

            return "anonymous";
        }
    }
}
