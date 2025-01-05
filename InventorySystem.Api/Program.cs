using InventorySystem.Api;
using InventorySystem.Api.ApiBaseServices;
using InventorySystem.Api.Filters;
using InventorySystem.Api.Middlewares;
using InventorySystem.Data.DataContext;
using InventorySystem.Mappers;
using InventorySystem.ServiceImplementation;
using InventorySystem.ServiceImplementation.Services.Core;
using InventorySystem.ServiceImplementation.UnitOfWork;
using InventorySystem.Shared.Entities.Enumerations.SeedEnumeration;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Models.Communications;
using InventorySystem.Shared.Models.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Security.Claims;
using System.Text;

try
{
    // Configure Default logger
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("D:\\Logs\\InventoryDemoApi\\Default\\Api-Default-.txt", rollingInterval: RollingInterval.Day)
        .CreateBootstrapLogger();

    var builder = WebApplication.CreateBuilder(args);

    Log.Information($"Application {builder.Environment.ApplicationName} / Environment: {builder.Environment.EnvironmentName} / MachineName: {Environment.MachineName}");

    // Load configuration files
    // ApiMessage Store and Translation translation 
    builder.Configuration.AddJsonFile("Messages.json", false, true);
    //Api ConfigExtention + Maintenance Mode + config for midelware + control for swagger
    builder.Configuration.AddJsonFile("AppConfig.json", false, true);

    // Retrieve Serilog configuration
    var serilogConfig = builder.Configuration.GetSection("Serilog");
    var minimumLevelString = serilogConfig.GetValue<string>("MinimumLevel");
    var minimumLevel = Enum.TryParse<LogEventLevel>(minimumLevelString, true, out var parsedMinimumLevel)
                       ? parsedMinimumLevel
                       : LogEventLevel.Verbose;

    var loggerConfig = new LoggerConfiguration()
        .MinimumLevel.Is(minimumLevel)
        .Enrich.FromLogContext()
        .WriteTo.Console();

    // Load File sink configuration dynamically
    var fileSink = serilogConfig.GetSection("WriteTo")
                                .GetChildren()
                                .FirstOrDefault(x => x.GetValue<string>("Name") == "File");

    if (fileSink != null)
    {
        var path = fileSink.GetSection("Args").GetValue<string>("path");
        var rollingInterval = fileSink.GetSection("Args").GetValue<RollingInterval>("rollingInterval");
        if (!string.IsNullOrEmpty(path))
        {
            loggerConfig.WriteTo.File(path, rollingInterval: rollingInterval);
        }
        else
        {
            Log.Warning("File sink path is not configured. Defaulting to console logging.");
        }
    }

    // Configure logger
    Log.Logger = loggerConfig.CreateLogger();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers(options =>
    {
        // Add a custom filter for validation if required
        options.Filters.Add(new ModelValidationAsyncActionFilter());
    }).AddNewtonsoftJson(options =>
    {
        // Set to ignore null values when writing JSON
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        // Ignore reference loops (prevents issues when serializing circular references)
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // Set the maximum depth for deserialization
        options.SerializerSettings.MaxDepth = 5; // Increase depth if needed for complex objects
    }).AddJsonOptions(options =>
    {
        // Set to ignore null values when writing JSON
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        // Handle reference loops by preserving object references (similar to ReferenceLoopHandling.Ignore in Newtonsoft.Json)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        // Set the maximum depth for deserialization
        options.JsonSerializerOptions.MaxDepth = 5; // Adjust based on your object complexity
    });

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        // Disable automatic model state validation (handled manually in a custom filter)
        options.SuppressModelStateInvalidFilter = true;
    });

    // Set up the maximum request body size globally for the API
    builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.MaxRequestBodySize = 5 * 1024 * 1024; // 5 MB limit
    });

    // For IIS (if hosting on IIS), you need to configure the limit in IIS as well
    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = 5 * 1024 * 1024; // 5 MB limit
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddMemoryCache();

    builder.Services.AddScoped<IHttpContextDataProvider, UserSessionDetailProvider>();
    builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));
    builder.Services.AddSingleton<ISmtpService, SmtpService>();
    builder.Services.AddSingleton<ITranslationService, TranslationService>();
    builder.Services.AddSingleton<ICustomMessageProvider, CustomMessageProvider>();
    builder.Services.AddScoped<IHelperService, ApiHelperService>();
    builder.Services.AddScoped<MapperService>();

    // Configure database context
    builder.Services.AddDbContext<DatabaseContext>();
    // Register custom services
    InitServices(builder.Services);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IAttachmentService, AttachmentService>();
    SwaggerConfig(builder);
    AuthenticationConfig(builder);
    builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    bool EnableSwagger = Convert.ToBoolean(builder.Configuration["EnableSwagger"] ?? "false");
    builder.Services.AddAuthorization(options =>
    {
        foreach (var permission in FeaturePermissionEnum.GetAllWithNames())
        {
            options.AddPolicy(permission.Key, policy =>
            policy.RequireAuthenticatedUser()
            .RequireClaim("UserModule", permission.Value!.Feature!.ModuleId.ToString())
            .RequireClaim("FeaturePermission", permission.Value.Id.ToString()));
        }
        foreach (var feature in FeatureEnum.GetAll())
        {
            var featurePermission = FeaturePermissionEnum.GetAll().Where(x => x.FeatureId == feature.Id).Select(x => x.Id).Distinct();
            List<string> featurepermissions = [];
            foreach (var id in featurePermission) { featurepermissions.Add(id.ToString()); }
            options.AddPolicy($"{feature.Name}_All", policy =>
            policy.RequireAuthenticatedUser()
            .RequireClaim("UserModule", feature.ModuleId.ToString())
            .RequireClaim("FeaturePermission", featurepermissions.ToArray()));
        }
    });

    var app = builder.Build();
    //for dev only to be removed 
    if (app.Environment.IsStaging() || app.Environment.IsDevelopment())
    {
        app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    }
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<MaintenanceMiddleware>();
    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseMiddleware<RequestValidationMiddleware>();
    app.UseMiddleware<DateTimeConversionMiddleware>();

    if (EnableSwagger)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            // Collapse sections by default for better performance on large APIs
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            // Disable syntax highlight to improve performance
            // options.ConfigObject.AdditionalItems["syntaxHighlight"] = false;

            // Use a theme if necessary
            // options.ConfigObject.AdditionalItems["theme"] = "agate";

            // Limit the model expand depth to avoid overwhelming the UI
            options.DefaultModelExpandDepth(3);

            // Limit displayed tags to reduce UI overhead
            // options.MaxDisplayedTags(3);
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    Log.Logger.Error(ex, "An error occurred while starting the application");
}

/// <summary>
/// Configures JWT authentication.
/// </summary>
static void AuthenticationConfig(WebApplicationBuilder builder)
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    builder.Services.Configure<JwtSettings>(jwtSettings);
    var jwtSettingsValue = builder.Configuration["Jwt:Key"];
    byte[] keyBytes;
    SymmetricSecurityKey subKeyForDecryption;
    if (!string.IsNullOrEmpty(jwtSettingsValue))
    {
        keyBytes = Encoding.UTF8.GetBytes(jwtSettingsValue);
        subKeyForDecryption = new SymmetricSecurityKey(keyBytes.Take(16).ToArray());
    }
    else
    {
        Log.Error("Jwt settings are not configured. Will not be able to use JWT. Please check appsettings.json file.");
        throw new Exception("Jwt settings are not configured. Will not be able to use JWT. Please check appsettings.json file.");
    }

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            TokenDecryptionKey = subKeyForDecryption,
            ClockSkew = TimeSpan.Zero,
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var accountService = context.HttpContext.RequestServices.GetRequiredService<IAccountService>();
                if (context.Principal == null || accountService == null)
                {
                    context.Fail("Unauthorized");
                    return;
                }
                if (context.Principal.Identity?.IsAuthenticated == false)
                {
                    context.Fail("Unauthorized");
                    return;
                }

                string? UserData = context.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
                if (UserData == null)
                {
                    context.Fail("Unauthorized");
                    return;
                }
                if (!UserData.Contains("Gest") && !UserData.Contains("User") && !UserData.Contains("Owner") && !UserData.Contains("Reset"))
                {
                    context.Fail("Unauthorized");
                    return;
                }
                if (UserData == "Gest")
                {
                    if (!accountService.LoginUserSignature(context.Principal!.Claims!.ToList()))
                    {
                        context.Fail("Unauthorized");
                        return;
                    }
                }
                if (UserData == "User")
                {
                    if (!await accountService.UserSignature(context.Principal!.Claims?.ToList()))
                    {
                        context.Fail("Unauthorized");
                        return;
                    }
                }
                if (UserData == "Owner")
                {
                    if (!accountService.VerifyOwner(context.Principal!.Claims!.ToList()))
                    {
                        context.Fail("Unauthorized");
                        return;
                    }
                }
                if (UserData == "Reset")
                {
                    if (!accountService.VerifyResetPwd(context.Principal!.Claims!.ToList()))
                    {
                        context.Fail("Unauthorized");
                        return;
                    }
                }
            }
        };
    });
}

/// <summary>
/// Configures Swagger for API documentation.
/// </summary>
/// 
static void SwaggerConfig(WebApplicationBuilder builder)
{
    var swaggerId = builder.Configuration["SwaggerId"] ?? "";

    // Add and configure Swagger generation
    builder.Services.AddSwaggerGen(c =>
    {
        // Security definition for JWT Bearer token
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT token into this field (To Get Token Check Account/Login)",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        // Security requirement for all operations
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        c.OrderActionsBy(apiDesc =>
        {
            var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];
            return controllerName switch
            {
                "Account" => "01_Account",
                "ApplicationManager" => "02_ApplicationManager",
                "AdminControlPanel" => "03_AdminControlPanel",
                "ClientControlPanel" => "04_ClientControlPanel",
                "AttachmentStore" => "05_AttachmentStore",
                "SystemMessage" => "06_SystemMessage",
                _ => controllerName
            };
        });

        c.EnableAnnotations();
        c.OperationFilter<CustomHeadersOperationFilter>(swaggerId);
    });
}

/// <summary>
/// Initializes custom services.
/// </summary>
static void InitServices(IServiceCollection services)
{
    services.AddLibraryServices(AssemblyReference.Assembly);
}