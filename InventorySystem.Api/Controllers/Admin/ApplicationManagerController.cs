using InventorySystem.Data.DataContext;
using InventorySystem.Mappers;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Tools;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InventorySystem.Api.Controllers.Admin
{
    /// <summary>
    /// Controller for managing application configurations and database updates.
    /// Restricted to users with the "ApplicationOwner" role.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleDefinitions.ApplicationOwner}")]
    public class ApplicationManagerController : ControllerBaseExt
    {
        private readonly ILogger<ApplicationManagerController> _logService;
        private readonly DatabaseContext _databaseContext;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IAccountService _accountService;
        private readonly MapperService _mapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManagerController"/> class.
        /// </summary>
        /// <param name="logService">Logger for tracking operations and errors.</param>
        /// <param name="databaseContext">Database context for performing database operations.</param>
        /// <param name="factory">Logger factory for creating loggers.</param>
        /// <param name="configuration">Application configuration for retrieving connection strings.</param>
        /// <param name="cache">Memory cache for temporary data storage.</param>
        /// <param name="accountService">Service for account-related operations.</param>
        /// <param name="mapperService">Service for handling data mappings.</param>
        public ApplicationManagerController(
            ILogger<ApplicationManagerController> logService,
            DatabaseContext databaseContext,
            ILoggerFactory factory,
            IConfiguration configuration,
            IMemoryCache cache,
            IAccountService accountService,
            MapperService mapperService)
        {
            _logService = logService;
            _databaseContext = databaseContext;
            _loggerFactory = factory;
            _configuration = configuration;
            _memoryCache = cache;
            _accountService = accountService;
            _mapperService = mapperService;
        }

        /// <summary>
        /// Generates an access token for the application owner.
        /// </summary>
        /// <returns>A boolean indicating whether the token was successfully generated.</returns>
        [AllowAnonymous, HttpGet("OwnerAccess")]
        public async Task<bool> GenerateAccessToken()
        {
            return await _accountService.GenerateOwnerAccessToken();
        }

        /// <summary>
        /// Updates the configuration database schema.
        /// </summary>
        /// <param name="runScriptUpdate">Specifies whether to run the database update scripts.</param>
        /// <returns>A string indicating the result of the update operation.</returns>
        [HttpPost("UpdateConfigurationDatabase")]
        public string UpdateConfigurationDatabase(bool runScriptUpdate = false)
        {
            return _databaseContext.UpdateDatabase(runScriptUpdate);
        }

        /// <summary>
        /// Executes database update scripts for the configuration database using a custom connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        /// <returns>A string indicating the result of the script execution.</returns>
        [HttpPost("UpdateConfigurationDatabaseScripts")]
        public string UpdateConfigurationDatabaseScripts(string connectionString)
        {
            return _databaseContext.ExecuteSqlScriptUpdate(connectionString);
        }

        /// <summary>
        /// Updates a specific database schema.
        /// </summary>
        /// <param name="DatabaseName">The name of the database to update.</param>
        /// <param name="runScriptUpdate">Specifies whether to run the database update scripts.</param>
        /// <returns>A string indicating the result of the update operation.</returns>
        [HttpPost("UpdateDatabase")]
        public string UpdateDatabase(string DatabaseName, bool runScriptUpdate = false)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection") ?? _configuration.GetConnectionString(DatabaseName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionString' not found in configuration.");
            }

            var provider = new DefaultConnectionStringProvider(connectionString, "0#owner");
            var context = new DatabaseContext(provider, _loggerFactory, _memoryCache, _configuration, _mapperService);

            return context.UpdateDatabase(runScriptUpdate, connectionString);
        }

        /// <summary>
        /// Executes database update scripts for a specific database using its name.
        /// </summary>
        /// <param name="DatabaseName">The name of the database.</param>
        /// <returns>A string indicating the result of the script execution.</returns>
        [HttpPost("UpdateDatabaseScripts")]
        public string UpdateDatabaseScripts(string DatabaseName)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection") ?? _configuration.GetConnectionString(DatabaseName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionString' not found in configuration.");
            }

            var provider = new DefaultConnectionStringProvider(connectionString, "0#owner");
            var context = new DatabaseContext(provider, _loggerFactory, _memoryCache, _configuration, _mapperService);

            return context.ExecuteSqlScriptUpdate(connectionString);
        }
    }
}
