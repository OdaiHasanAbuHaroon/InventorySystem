using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace InventorySystem.Data.Interceptors
{
    public class CachingInterceptor : DbCommandInterceptor
    {
        private readonly IMemoryCache _memoryCache;

        public CachingInterceptor(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // Implement the logic for caching if necessary
        // You can override methods from DbCommandInterceptor to add caching logic
    }
}
