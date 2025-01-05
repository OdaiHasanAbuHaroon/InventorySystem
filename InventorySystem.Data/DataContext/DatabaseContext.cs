using InventorySystem.Mappers;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Entities.Enumerations.SeedEnumeration;
using InventorySystem.Shared.EntitiesConfiguration.Business;
using InventorySystem.Shared.EntitiesConfiguration.Core;
using InventorySystem.Shared.EntitiesConfiguration.Identity;
using InventorySystem.Shared.Interfaces.Interceptors;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Data.Interceptors;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace InventorySystem.Data.DataContext
{
    public class DatabaseContext : DbContext
    {

        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly ILogger<DatabaseContext> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private readonly MapperService Mappers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">Provides the connection string for the database.</param>
        /// <param name="eyeConfigurationsContext">The configuration context for shared information.</param>
        /// <param name="loggerFactory">The logger factory to create loggers.</param>
        /// <param name="memoryCache">The memory cache to use for caching.</param>
        public DatabaseContext(IHttpContextDataProvider connectionStringProvider, ILoggerFactory loggerFactory, IMemoryCache memoryCache, IConfiguration configuration, MapperService mapper)
        {
            _contextDataProvider = connectionStringProvider;
            _logger = loggerFactory.CreateLogger<DatabaseContext>();
            _memoryCache = memoryCache;
            _loggerFactory = loggerFactory;
            _configuration = configuration;
            Mappers = mapper;

        }

        #region DbSets

        #region Core

        public virtual DbSet<SeriLogEvent> SeriLogEvents { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<AttachmentBackup> AttachmentBackups { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<TimeZoneInformation> TimeZoneInformations { get; set; }

        public virtual DbSet<Theme> Themes { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        #endregion

        #region Business

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<ItemStatus> ItemStatuses { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<SerialNumber> SerialNumbers { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        #endregion

        #region Identity

        public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }

        public virtual DbSet<GroupFeaturePermission> GroupFeaturePermissions { get; set; }

        public virtual DbSet<UserSecurityGroup> UserSecurityGroups { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<Feature> Features { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<FeaturePermission> FeaturePermissions { get; set; }

        public virtual DbSet<UserModule> UserModules { get; set; }

        public virtual DbSet<UserFeaturePermission> UserFeaturePermissions { get; set; }

        public virtual DbSet<Twofactor> Twofactors { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// Configures the DbContext options.
        /// </summary>
        /// <param name="optionsBuilder">The builder used to create or modify options for this context.</param>
        /// <param name="connectionString">The connection string for the database.</param>
        /// <param name="assemblyName">The name of the assembly containing migrations.</param>
        private void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder, string connectionString, string assemblyName)
        {
            try
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
                if (_contextDataProvider != null)
                {
                    optionsBuilder.AddInterceptors(new TimeAuditableInterceptor(_contextDataProvider), new TimeLessAuditableInterceptor(_contextDataProvider));
                }
                if (_loggerFactory != null)
                {
                    optionsBuilder.UseLoggerFactory(_loggerFactory);
                }
                optionsBuilder.UseSqlServer(connectionString, o =>
                {
                    o.MigrationsAssembly(assemblyName);
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    o.CommandTimeout(500);
                    o.UseNetTopologySuite();

                })
                .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
        }

        /// <summary>
        /// Configures the database (and other options) to be used for this context.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    if (_contextDataProvider != null)
                    {
                        string connectionstring = _configuration.GetConnectionString("DefaultConnection") ?? _contextDataProvider.GetConnectionString();
                        ConfigureDbContext(optionsBuilder, connectionstring, typeof(DatabaseContext).Assembly.GetName().Name ?? "InventorySystem.Data");
                    }
                }
                optionsBuilder.AddInterceptors(new CachingInterceptor(_memoryCache));
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }

        }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.HasDefaultSchema("Application");
                EntitiesConfig(modelBuilder);
                if (_configuration.GetValue<bool>("ClientMigration:EnableDefaultSeed"))
                {
                    _logger.LogInformation("Default Seed Start...");
                    DefaultSeed(modelBuilder);
                    _logger.LogInformation("Default Seed completed successfully.");
                }
                if (_configuration.GetValue<bool>("ClientMigration:EnableCustomSeed"))
                {
                    ExecuteSqlScriptUpdate(null);
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
        }

        /// <summary>
        /// Configures entities within the model builder.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected void EntitiesConfig(ModelBuilder modelBuilder)
        {

            #region Identity

            modelBuilder.ApplyConfiguration(new FeatureConfiguration());

            modelBuilder.ApplyConfiguration(new FeaturePermissionConfiguration());

            modelBuilder.ApplyConfiguration(new GroupFeaturePermissionConfiguration());

            modelBuilder.ApplyConfiguration(new ModuleConfiguration());

            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new SecurityGroupConfiguration());

            modelBuilder.ApplyConfiguration(new TwofactorConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new UserFeaturePermissionConfiguration());

            modelBuilder.ApplyConfiguration(new UserModuleConfiguration());

            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new UserSecurityGroupConfiguration());

            #endregion

            #region Core

            modelBuilder.ApplyConfiguration(new AttachmentBackupConfiguration());

            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());

            modelBuilder.ApplyConfiguration(new CountryConfiguration());

            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            modelBuilder.ApplyConfiguration(new SeriLogEventConfiguration());

            modelBuilder.ApplyConfiguration(new ThemeConfiguration());

            modelBuilder.ApplyConfiguration(new TimeZoneInformationConfiguration());

            #endregion

            #region Business

            modelBuilder.ApplyConfiguration(new BrandConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ItemConfiguration());

            modelBuilder.ApplyConfiguration(new ItemStatusConfiguration());

            modelBuilder.ApplyConfiguration(new LocationConfiguration());

            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());

            modelBuilder.ApplyConfiguration(new SerialNumberConfiguration());

            modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            #endregion
        }

        /// <summary>
        /// Seeds the database with default values.
        /// This method initializes default values for entities such as language, country, roles, modules, permissions, default super admin.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected void DefaultSeed(ModelBuilder modelBuilder)
        {
            try
            {
                var seedDate = DateTime.UtcNow;
                modelBuilder.Entity<Country>().HasData(CountryEnum.GetAll().ToArray());
                modelBuilder.Entity<Currency>().HasData(CurrencyEnum.GetAll().ToArray());
                modelBuilder.Entity<Language>().HasData(LanguagesEnum.GetAll().ToArray());
                modelBuilder.Entity<TimeZoneInformation>().HasData(TimeZoneInformationEnum.GetAll().ToArray());
                Theme defaultTheme = new() { IsDefault = true, Name = "default", Color = "#000000", CreatedBy = "InitSeed", CreatedDate = DateTime.UtcNow, FontSize = 12, Id = 1, IsActive = true, IsDeleted = false };
                modelBuilder.Entity<Theme>().HasData(defaultTheme);
                modelBuilder.Entity<Role>().HasData(RoleEnum.GetAll().ToArray());
                modelBuilder.Entity<Module>().HasData(ModuleEnum.GetAll().ToArray());
                modelBuilder.Entity<Feature>().HasData(FeatureEnum.GetAll().ToArray());
                modelBuilder.Entity<Permission>().HasData(PermissionEnum.GetAll().ToArray());
                List<FeaturePermission> featurePermissions = [];

                foreach (var item in FeaturePermissionEnum.GetAll())
                {
                    featurePermissions.Add(new FeaturePermission() { FeatureId = item.FeatureId, Id = item.Id, PermissionId = item.PermissionId });
                }
                modelBuilder.Entity<FeaturePermission>().HasData(featurePermissions.ToArray());
                modelBuilder.Entity<SecurityGroup>().HasData(SecurityGroupEnum.GetAll().ToArray());
                //SecurityGroupEnum
                User superAdmin = new()
                {
                    DateOfBirth = new DateTime(1991, 9, 6),
                    Gender = "Male",
                    LanguageId = 1,
                    ThemeId = 1,
                    TimeZone_InfoId = 1,
                    Address = "Address",
                    UserFontSize = 18,
                    CreatedDate = seedDate,
                    IsDeleted = false,
                    Email = "no-reply@gmail.com",
                    AccessFaildCount = 0,
                    EmailConfirmed = true,
                    IsActive = true,
                    LookoutEnabled = true,
                    PhoneNumber = "66446400",
                    MobileNumberConfirmed = true,
                    ModifiedDate = seedDate,
                    PasswordHash = "aa35e0676e24993a52eb1e251bfb820ff5f139c5d48890da51c4ad8a1ba2b3d4197294145a253d66af5ddda545d76af7e72f627117a413f7b5ca720d17b45362",
                    TwoFactorEnabled = true,
                    CreatedBy = "Init Seed",
                    Id = 1,
                    Lookout = false,
                    SmsEnabled = true,
                    FirstName = "SuperAdministrator",
                    LastName = "",
                };

                modelBuilder.Entity<User>().HasData(superAdmin);
                modelBuilder.Entity<UserSecurityGroup>().HasData(new UserSecurityGroup() { SecurityGroupId = SecurityGroupEnum.SuperAdminGroup.Id, UserId = superAdmin.Id, CreatedBy = "Init Seed", CreatedDate = seedDate, });
                List<UserRole> userRoles = new() { new UserRole() { UserId = 1, RoleId = RoleEnum.SuperAdministrator.Id, CreatedBy = "Init Seed", CreatedDate = seedDate } };
                modelBuilder.Entity<UserRole>().HasData(userRoles.ToArray());
                List<GroupFeaturePermission> userGroupPermission = [];
                foreach (var permission in FeaturePermissionEnum.GetAll())
                {
                    userGroupPermission.Add(new GroupFeaturePermission() { FeaturePermissionId = permission.Id, SecurityGroupId = SecurityGroupEnum.SuperAdminGroup.Id });
                }
                modelBuilder.Entity<GroupFeaturePermission>().HasData(userGroupPermission.ToArray());
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
            }
        }

        public string UpdateDatabase(bool RunScriptUpdate = false, string? connectionString = null)
        {
            StringBuilder LogMessage = new();
            _logger.LogInformation("Fetching Pending Migrations...");
            LogMessage.AppendLine("Fetching Pending Migrations...");

            try
            {
                if (Database.GetPendingMigrations().Any())
                {
                    foreach (var migration in Database.GetPendingMigrations())
                    {
                        _logger.LogInformation($"Pending migration: {migration}");
                        LogMessage.AppendLine($"Pending migration: {migration}");
                    }
                    Database.Migrate();
                    _logger.LogInformation("Applying Migration completed successfully.");
                    LogMessage.AppendLine("Applying Migration completed successfully.");
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                LogMessage.AppendLine(JsonConvert.SerializeObject(exp, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = 3 }));
            }

            if (RunScriptUpdate)
            {
                string result = ExecuteSqlScriptUpdate(connectionString);
                _logger.LogInformation(result);
                LogMessage.AppendLine(result);
            }

            _logger.LogInformation("Update completed successfully.");
            LogMessage.AppendLine("Update completed successfully.");
            return LogMessage.ToString();
        }

        public string ExecuteSqlScriptUpdate(string? connectionStringParam)
        {
            StringBuilder LogMessage = new();
            try
            {
                string startMessage = "Starting Script Update ...";
                string endMessage = "End Script Update.";
                string exportFunction = "Start Create/Alter Functions";
                string exportProcedure = "Start Create/Alter Procedures";

                // Get the connection string from the database context
                string? connectionString = connectionStringParam ?? _configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    _logger.LogInformation("Invalid connection string!");
                    LogMessage.AppendLine("Invalid connection string!");
                    return LogMessage.ToString();
                }

                // Log start message
                _logger.LogInformation(startMessage);
                LogMessage.AppendLine(startMessage);

                // Get the assembly location
                var assemblyLocation = Path.GetDirectoryName(typeof(DatabaseContext).Assembly.Location);
                _logger.LogInformation($"Assembly.Location: {assemblyLocation}");
                LogMessage.AppendLine($"Assembly.Location: {assemblyLocation}");

                // Get the script path from configuration
                var scriptsPath = _configuration.GetValue<string>("ScriptsPath");
                _logger.LogInformation($"ScriptsPath settings: {scriptsPath}");
                LogMessage.AppendLine($"ScriptsPath settings: {scriptsPath}");

                // Combine assembly location with the configured script path
                var fullScriptsPath = Path.Combine(assemblyLocation, scriptsPath);
                _logger.LogInformation($"FullScriptsPath: {fullScriptsPath}");
                LogMessage.AppendLine($"FullScriptsPath: {fullScriptsPath}");

                // Check if the directory exists
                if (!Directory.Exists(fullScriptsPath))
                {
                    var errorMessage = $"Scripts directory not found at path: {fullScriptsPath}";
                    _logger.LogError(errorMessage);
                    LogMessage.AppendLine(errorMessage);
                    return LogMessage.ToString();
                }

                // Run Functions
                _logger.LogInformation(exportFunction);
                LogMessage.AppendLine(exportFunction);
                RunSqlScriptsUsingSqlCommand(LogMessage, connectionString, Path.Combine(fullScriptsPath, "Functions"));

                // Run StoredProcedures
                _logger.LogInformation(exportProcedure);
                LogMessage.AppendLine(exportProcedure);
                RunSqlScriptsUsingSqlCommand(LogMessage, connectionString, Path.Combine(fullScriptsPath, "StoredProcedures"));

                // Log end message
                _logger.LogInformation(endMessage);
                LogMessage.AppendLine(endMessage);

                return LogMessage.ToString();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                LogMessage.AppendLine(JsonConvert.SerializeObject(exp, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = 5 }));
                return LogMessage.ToString();
            }
        }

        private void RunSqlScriptsUsingSqlCommand(StringBuilder LogMessage, string connectionString, string scriptsPath)
        {
            try
            {
                // Check if the directory exists
                if (!Directory.Exists(scriptsPath))
                {
                    var directoryNotFoundMessage = $"Directory not found: {scriptsPath}";
                    _logger.LogWarning(directoryNotFoundMessage);
                    LogMessage.AppendLine(directoryNotFoundMessage);
                    return;
                }

                // Get all SQL files
                var sqlFiles = Directory.GetFiles(scriptsPath, "*.sql");

                if (sqlFiles.Length == 0)
                {
                    var noFilesMessage = $"No SQL scripts found in the directory: {scriptsPath}";
                    _logger.LogInformation(noFilesMessage);
                    LogMessage.AppendLine(noFilesMessage);
                    return;
                }

                // Execute each SQL file
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                foreach (var filePath in sqlFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    _logger.LogInformation($"Executing script: {fileName}");
                    LogMessage.AppendLine($"Executing script: {fileName}");

                    var sqlContent = File.ReadAllText(filePath);
                    if (string.IsNullOrWhiteSpace(sqlContent))
                    {
                        _logger.LogWarning($"SQL script {fileName} is empty, skipping.");
                        LogMessage.AppendLine($"SQL script {fileName} is empty, skipping.");
                        continue;
                    }

                    try
                    {
                        using (var command = new SqlCommand(sqlContent, connection))
                        {
                            command.CommandTimeout = 600;  // Set a higher timeout if needed
                            command.ExecuteNonQuery();
                        }

                        _logger.LogInformation($"Successfully executed script: {fileName}");
                        LogMessage.AppendLine($"Successfully executed script: {fileName}");
                    }
                    catch (Exception fileException)
                    {
                        _logger.LogError(fileException, $"Error executing script: {filePath}");
                        LogMessage.AppendLine($"Error executing script: {filePath}");
                        LogMessage.AppendLine(JsonConvert.SerializeObject(fileException, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = 5 }));
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, $"Error executing scripts in path: {scriptsPath}");
                LogMessage.AppendLine($"Error executing scripts in path: {scriptsPath}");
                LogMessage.AppendLine(JsonConvert.SerializeObject(exp, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, MaxDepth = 5 }));
            }
        }
    }
}
