using InventorySystem.Shared.Interfaces.Interceptors;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InventorySystem.Data.Interceptors
{
    public class TimeAuditableInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextDataProvider _contextDataProvider;
        // public TimeAuditableInterceptor() { }
        public TimeAuditableInterceptor(IHttpContextDataProvider connectionStringProvider)
        {
            _contextDataProvider = connectionStringProvider;
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context!;
            var entries = context.ChangeTracker.Entries<ITimeAuditableEntity>();

            foreach (var entry in entries)
            {
                // Handle CreatedDate and ModifiedDate as before
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _contextDataProvider.GetCurrentUserId();
                    entry.Entity.Source = _contextDataProvider.GetUserAgent();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = _contextDataProvider.GetCurrentUserId();
                    entry.Entity.Source = _contextDataProvider.GetUserAgent();
                }

                //foreach (var property in entry.Properties)
                //{
                //    if (property.Metadata.ClrType == typeof(DateTime) &&
                //        property.Metadata.Name != nameof(ITimeAuditableEntity.CreatedDate) &&
                //        property.Metadata.Name != nameof(ITimeAuditableEntity.ModifiedDate))
                //    {
                //        DateTime? currentValue = (DateTime?)property.CurrentValue;
                //        if (currentValue.HasValue)
                //        {
                //            try
                //            {
                //                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(_contextDataProvider.GetTimeZone() ?? "UTC");
                //                property.CurrentValue = TimeZoneInfo.ConvertTimeToUtc(currentValue.Value, timeZone);
                //            }
                //            catch (Exception exp)
                //            {
                //                Console.WriteLine($"Invalid time zone data: {exp.Message}");
                //                property.CurrentValue = currentValue.Value.ToUniversalTime();
                //            }
                //        }
                //    }
                //}
            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
