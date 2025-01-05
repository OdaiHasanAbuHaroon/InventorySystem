using InventorySystem.Mappers;
using InventorySystem.Shared.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InventorySystem.Data.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ContextConfig.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
            }

            Log.Logger = new LoggerConfiguration()
             .ReadFrom.Configuration(configuration)
             .CreateLogger();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(connectionString, o =>
            {
                o.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                o.CommandTimeout(500);
                o.UseNetTopologySuite();
            }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            var Provider = new DefaultConnectionStringProvider(connectionString, "1");
            var Mapper = new MapperService(Provider, loggerFactory);
            return new DatabaseContext(Provider, loggerFactory, new MemoryCache(new MemoryCacheOptions()), configuration, Mapper);
        }
    }
}
