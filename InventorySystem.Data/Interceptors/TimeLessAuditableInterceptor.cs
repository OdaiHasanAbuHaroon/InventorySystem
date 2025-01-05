using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InventorySystem.Shared.Interfaces.Interceptors
{
    public class TimeLessAuditableInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextDataProvider _contextDataProvider;
        // public TimeLessAuditableInterceptor() { }
        public TimeLessAuditableInterceptor(IHttpContextDataProvider connectionStringProvider)
        {
            _contextDataProvider = connectionStringProvider;
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context!;
            var entries = context.ChangeTracker.Entries<ITimeLessAuditableEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _contextDataProvider.GetCurrentUserId();
                    entry.Entity.Source = _contextDataProvider.GetUserAgent();
                }
                //foreach (var property in entry.Properties)
                //{
                //    if (property.Metadata.ClrType == typeof(DateTime) && property.Metadata.Name != nameof(ITimeAuditableEntity.CreatedDate))
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
