using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.Interfaces.Providers;

namespace InventorySystem.Shared.Tools
{
    public class DefaultConnectionStringProvider : IHttpContextDataProvider
    {
        private readonly string _connectionString;
        private readonly string _userId = "1";

        public DefaultConnectionStringProvider(string connectionString, string? userId = null)
        {
            _connectionString = connectionString;
            if (!string.IsNullOrEmpty(userId)) { _userId = userId; }
        }

        public string GetConnectionString() => _connectionString;
        public string GetCurrentUserId() => _userId;

        public ContextUserInfo? GetContextUserInfo()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserEmail()
        {
            throw new NotImplementedException();
        }

        public string GetTimeZone()
        {
            return "UTC";
        }

        public string GetUserAgent()
        {
            return "Local";
        }
    }
}
