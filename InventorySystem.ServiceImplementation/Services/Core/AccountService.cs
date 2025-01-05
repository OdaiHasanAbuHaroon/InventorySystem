using InventorySystem.Data.DataContext;
using InventorySystem.Mappers;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Models.Communications;
using InventorySystem.Shared.Models.Jwt;
using InventorySystem.Shared.Responses;
using InventorySystem.Shared.Tools;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<AccountService> _logService;
        private readonly IConfiguration _configuration;
        private readonly ICustomMessageProvider _messageProvider;
        private readonly JwtSettings _jwtSettings;
        private readonly IAttachmentService _attachmentService;
        private readonly IHelperService _helpService;
        private readonly ISmtpService _smtpService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperService Mappers;

        protected int MaxFileSizeInBytes = 5 * 1024 * 1024; // 5MB

        public AccountService(
            IOptions<JwtSettings> JwtOptions,
            IHelperService helpService,
            ISmtpService smtpService,
            DatabaseContext context,
            IConfiguration configuration,
            ILogger<AccountService> logsService,
            ICustomMessageProvider customMessage,
            IHostEnvironment webHost,
            IAttachmentService attachment,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _logService = logsService;
            _configuration = configuration;
            _messageProvider = customMessage;
            _jwtSettings = JwtOptions.Value;
            _attachmentService = attachment;
            MaxFileSizeInBytes = _configuration.GetValue("MaxFileSizeInBytes", 5242880);
            _helpService = helpService;
            _smtpService = smtpService;
            _unitOfWork = unitOfWork;
            Mappers = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Retrieves detailed user information by email, including associated roles, modules, feature permissions, 
        /// and security groups, while filtering out deleted records.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>
        /// A Task containing a User object with detailed associations, or null if no active user with the provided email exists.
        /// </returns>
        private async Task<User?> GetUserDataByEmail(string email)
        {
            try
            {
                var result = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(u => u.UserModules)
                        .ThenInclude(um => um.Module)
                    .Include(u => u.UserFeaturePermissions)
                        .ThenInclude(ufp => ufp.FeaturePermission)
                            .ThenInclude(fp => fp!.Feature)
                                .ThenInclude(f => f!.Module)
                    .Include(u => u.UserFeaturePermissions)
                        .ThenInclude(ufp => ufp.FeaturePermission)
                            .ThenInclude(fp => fp!.Permission)
                    .Include(u => u.UserSecurityGroups)
                        .ThenInclude(usg => usg.SecurityGroup)
                            .ThenInclude(sg => sg!.GroupFeaturePermissions)
                                .ThenInclude(gfp => gfp.FeaturePermission)
                                    .ThenInclude(fp => fp!.Feature)
                                        .ThenInclude(f => f!.Module)
                    .Include(u => u.UserSecurityGroups)
                        .ThenInclude(usg => usg.SecurityGroup)
                            .ThenInclude(sg => sg!.GroupFeaturePermissions)
                                .ThenInclude(gfp => gfp.FeaturePermission)
                                    .ThenInclude(fp => fp!.Permission)
                    .Where(u => u.Email == email && !u.IsDeleted && u.IsActive)
                    .AsTracking()
                    .FirstOrDefaultAsync();
                if (result != null)
                {
                    result.UserFeaturePermissions = result.UserFeaturePermissions.Where(x => x.IsDeleted == false).ToList();
                    result.UserSecurityGroups = result.UserSecurityGroups.Where(x => x.IsDeleted == false).ToList();
                    result.UserRoles = result.UserRoles.Where(x => x.IsDeleted == false).ToList();
                    result.UserModules = result.UserModules.Where(x => x.IsDeleted == false).ToList();
                }
                return result;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// Retrieves detailed user information by user ID, including associated roles, modules, feature permissions, 
        /// and security groups, while filtering out deleted records. The function also ensures the user is active 
        /// and not currently locked out.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>
        /// A Task containing a User object with detailed associations, or null if no matching active user is found.
        /// </returns>
        private async Task<User?> GetUserDataById(long id)
        {
            var result = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.UserModules)
                    .ThenInclude(um => um.Module)
                .Include(u => u.UserFeaturePermissions)
                    .ThenInclude(ufp => ufp.FeaturePermission)
                        .ThenInclude(fp => fp!.Feature)
                            .ThenInclude(f => f!.Module)
                .Include(u => u.UserFeaturePermissions)
                    .ThenInclude(ufp => ufp.FeaturePermission)
                        .ThenInclude(fp => fp!.Permission)
                .Include(u => u.UserSecurityGroups)
                    .ThenInclude(usg => usg.SecurityGroup)
                        .ThenInclude(sg => sg!.GroupFeaturePermissions)
                            .ThenInclude(gfp => gfp.FeaturePermission)
                                .ThenInclude(fp => fp!.Feature)
                                    .ThenInclude(f => f!.Module)
                .Include(u => u.UserSecurityGroups)
                    .ThenInclude(usg => usg.SecurityGroup)
                        .ThenInclude(sg => sg!.GroupFeaturePermissions)
                            .ThenInclude(gfp => gfp.FeaturePermission)
                                .ThenInclude(fp => fp!.Permission)
                .Where(u => u.Id == id && !u.IsDeleted && u.IsActive && (u.LookoutEnd == null || u.LookoutEnd < DateTime.UtcNow))
                 .AsTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                result.UserFeaturePermissions = result.UserFeaturePermissions.Where(x => x.IsDeleted == false).ToList();
                result.UserSecurityGroups = result.UserSecurityGroups.Where(x => x.IsDeleted == false).ToList();
                result.UserRoles = result.UserRoles.Where(x => x.IsDeleted == false).ToList();
                result.UserModules = result.UserModules.Where(x => x.IsDeleted == false).ToList();

            }

            return result;
        }

        /// <summary>
        /// Handles the login process for a user, including validation of credentials, checking for account lockout, 
        /// and optionally triggering two-factor authentication if enabled. Generates a JWT token upon successful login.
        /// </summary>
        /// <param name="accountLoginForm">The login form data containing email and password.</param>
        /// <param name="lang">The language for localized messages.</param>
        /// <param name="timeZone">The user's time zone for token expiration adjustment.</param>
        /// <returns>
        /// A Task containing a <see cref="LoginResponse"/> object, which includes details of the login process, 
        /// such as success status, messages, and tokens for authentication.
        /// </returns>
        public async Task<LoginResponse> Login(LoginFormData accountLoginForm, string lang, string timeZone)
        {
            try
            {
                User? baseUser = await GetUserDataByEmail(accountLoginForm.Email);
                if (baseUser == null)
                {
                    return new LoginResponse
                    {
                        MultiFactorRequired = false,
                        Success = false,
                        Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.email_not_found, lang),
                        Response = null,
                        StatusCode = ResponseMessageCode.ErrorStatusCode
                    };
                }

                if (baseUser.PasswordHash != Shared.Tools.Utility.GenerateSha512Hash(accountLoginForm.Password))
                {
                    return await HandleInvalidPasswordAsync(baseUser, lang);
                }

                if (baseUser.Lookout)
                {
                    return new LoginResponse
                    {
                        MultiFactorRequired = false,
                        Success = false,
                        Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.account_locked, lang),
                        Response = null,
                        StatusCode = ResponseMessageCode.ErrorStatusCode
                    };
                }

                if (baseUser.TwoFactorEnabled)
                {
                    var TwoFA = await GenerateAndSendTwoFactorCodeAsync(baseUser);
                    if (TwoFA != null)
                    {
                        return new LoginResponse
                        {
                            MultiFactorRequired = true,
                            Success = true,
                            Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.two_factor_required, lang),
                            Response = null,
                            StatusCode = ResponseMessageCode.SuccessStatusCode,
                            Token = GenerateJwtToken2fa(GenerateUserClaimForLogin(baseUser, TwoFA.Stamp)),
                            TokenExpiration = Shared.Tools.Utility.ConvertToUserTimezone(DateTime.UtcNow.AddMinutes(5), timeZone)
                        };
                    }

                    return new LoginResponse
                    {
                        MultiFactorRequired = false,
                        Success = false,
                        Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.otp_error, lang),
                        Response = null,
                        StatusCode = ResponseMessageCode.ErrorStatusCode
                    };
                }
                else
                {
                    return await LoginProcess(lang, baseUser, timeZone);
                }
            }
            catch (Exception exp)
            {

                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return new LoginResponse
                {
                    MultiFactorRequired = false,
                    Success = false,
                    Message = exp.Message,
                    Response = null,
                    StatusCode = ResponseMessageCode.ErrorStatusCode
                };
            }
        }

        /// <summary>
        /// Processes a successful login for a user by updating login details, resetting access failure counts, 
        /// and generating a JWT token for the authenticated session. Also maps user profile data to the response.
        /// </summary>
        /// <param name="lang">The language for localized messages.</param>
        /// <param name="baseUser">The user object retrieved during login.</param>
        /// <param name="timeZone">The user's time zone for adjusting token expiration time.</param>
        /// <returns>
        /// A Task containing a <see cref="LoginResponse"/> object, including the login status, success message, 
        /// user profile data, authentication token, and token expiration time.
        /// </returns>
        private async Task<LoginResponse> LoginProcess(string lang, User baseUser, string timeZone)
        {
            string? ConfigLifetTime = _configuration.GetValue<string>("SessionLifeTime");
            int SessionLifeTime = int.Parse(ConfigLifetTime ?? "60");
            baseUser.LastLoginDate = DateTime.UtcNow;
            baseUser.AccessFaildCount = 0;
            baseUser.Lookout = false;
            if (baseUser.LookoutEnabled) { baseUser.LookoutEnd = DateTime.UtcNow; }

            var claims = GenerateUserClaim(baseUser, SessionLifeTime);
            var HashClaim = claims.Where(x => x.Type == ClaimTypes.Hash).FirstOrDefault();
            if (HashClaim != null)
            {
                baseUser.Signature = HashClaim.Value;
            }
            else
            {
                baseUser.Signature = "*";
            }
            _context.Users.Update(baseUser);
            await _context.SaveChangesAsync();
            // UserProfileMapper profileMapper = new UserProfileMapper();
            return new LoginResponse
            {
                MultiFactorRequired = false,
                Success = true,
                Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.login_success, lang),
                Response = Mappers.UserProfileMapper.DataToProfile(baseUser),
                StatusCode = ResponseMessageCode.SuccessStatusCode,
                Token = GenerateJwtToken(claims, SessionLifeTime),
                TokenExpiration = Shared.Tools.Utility.ConvertToUserTimezone(DateTime.UtcNow.AddMinutes(SessionLifeTime - 1), timeZone)
            };
        }

        /// <summary>
        /// Handles the scenario where an invalid password is provided during login.
        /// Updates the user's access failure count and locks the account if the failure threshold is exceeded.
        /// </summary>
        /// <param name="baseUser">The user object associated with the login attempt.</param>
        /// <param name="lang">The language for localized messages.</param>
        /// <returns>
        /// A Task containing a <see cref="LoginResponse"/> object with the login failure status, 
        /// error message, and lockout status if applicable.
        /// </returns>
        private async Task<LoginResponse> HandleInvalidPasswordAsync(User baseUser, string lang)
        {
            baseUser.AccessFaildCount += 1;

            if (baseUser.AccessFaildCount > 5)
            {
                if (baseUser.LookoutEnabled)
                {
                    baseUser.Lookout = true;
                    baseUser.LookoutEnd = DateTime.UtcNow.AddDays(7);
                }
            }
            _context.Users.Update(baseUser);
            await _context.SaveChangesAsync();

            string message = baseUser.Lookout ? MessageKeys.AccountServiceMessages.account_locked : MessageKeys.invalid_parameter;
            return new LoginResponse
            {
                MultiFactorRequired = false,
                Success = false,
                Message = _messageProvider.Find(message, lang),
                Response = null,
                StatusCode = ResponseMessageCode.ErrorStatusCode
            };
        }

        /// <summary>
        /// Generates a new two-factor authentication (2FA) code for the specified user, 
        /// deactivates any previous active codes, and attempts to send the new code via email.
        /// </summary>
        /// <param name="baseUser">The user for whom the 2FA code is generated.</param>
        /// <returns>
        /// A Task containing the generated <see cref="Twofactor"/> object if the operation succeeds, or null if an error occurs.
        /// </returns>
        private async Task<Twofactor?> GenerateAndSendTwoFactorCodeAsync(User baseUser)
        {
            try
            {
                var oldOtp = await _context.Twofactors.Where(x => x.UserId == baseUser.Id && !x.IsUsed && x.IsActive).AsTracking().ToListAsync();
                foreach (var otp in oldOtp)
                {
                    otp.IsActive = false;
                }
                _context.Twofactors.UpdateRange(oldOtp);
                await _context.SaveChangesAsync();
                Twofactor twofactor = new()
                {
                    Code = Shared.Tools.Utility.GenerateRandomPassword(8, false, false, true, false),
                    CreatedBy = "System",
                    CreatedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                    IsActive = true,
                    IsDeleted = false,
                    IsSent = true,
                    IsUsed = false,
                    UserId = baseUser.Id,
                    Stamp = Guid.NewGuid().ToString(),
                    RequestType = TwofactorRequestTypeDefinitions.LoginRequest
                };

                bool isSent = false;
                if (baseUser.EmailConfirmed && !string.IsNullOrEmpty(baseUser.Email))
                {
                    EmailFormModel emailFormModel = new()
                    {
                        Application = "Application",
                        To = baseUser.Email,
                        Body = $"<p>Your OTP for 2FA login is {twofactor.Code}. It's valid for 5 minutes. Please do not share this code with anyone</p>",
                        From = "no-reply@gmail.com",
                        Title = "2FA Login",
                        Attachments = "",
                        SendDate = null
                    };
                    string? emailChannel = _configuration.GetValue<string>("EmailChannel");
                    if (emailChannel != null)
                    {
                        if (emailChannel == EmailChannelDefinitions.SmtpChannel)
                        {
                            isSent = isSent || await _smtpService.SendEmailAsync(emailFormModel.To, emailFormModel.Title, emailFormModel.Body, null, true);
                        }
                        else
                        {
                            isSent = isSent || false;
                        }
                    }
                    else
                    {
                        isSent = isSent || false;
                    }
                }

                if (isSent)
                {
                    await _context.Twofactors.AddAsync(twofactor);
                    await _context.SaveChangesAsync();
                    return twofactor;
                }

                await _context.Twofactors.AddAsync(twofactor);
                await _context.SaveChangesAsync();

                return twofactor;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Generates a new two-factor authentication (2FA) code for account reset, 
        /// deactivates any previous active codes for the user, and attempts to send the new code via email.
        /// </summary>
        /// <param name="baseUser">The user for whom the 2FA code is generated.</param>
        /// <returns>
        /// A Task containing the generated <see cref="Twofactor"/> object if the code is successfully sent via email, or null otherwise.
        /// </returns>
        private async Task<Twofactor?> GenerateAndSendTwoFactorCodeForReset(User baseUser)
        {
            try
            {
                var oldOtp = await _context.Twofactors.Where(x => x.UserId == baseUser.Id && !x.IsUsed && x.IsActive).ToListAsync();
                foreach (var otp in oldOtp)
                {
                    otp.IsActive = false;
                }
                _context.Twofactors.UpdateRange(oldOtp);
                await _context.SaveChangesAsync();
                Twofactor twofactor = new()
                {
                    Code = Shared.Tools.Utility.GenerateRandomPassword(8, false, false, true, false),
                    CreatedBy = "System",
                    CreatedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                    IsActive = true,
                    IsDeleted = false,
                    IsSent = true,
                    IsUsed = false,
                    UserId = baseUser.Id,
                    Stamp = Guid.NewGuid().ToString(),
                    RequestType = TwofactorRequestTypeDefinitions.ResetRequest
                };

                bool isSent = false;
                if (baseUser.EmailConfirmed && !string.IsNullOrEmpty(baseUser.Email))
                {
                    EmailFormModel emailFormModel = new()
                    {
                        Application = "Application",
                        To = baseUser.Email,
                        Body = $"<p>Your OTP for Account Reset is {twofactor.Code}. It's valid for 5 minutes. Please do not share this code with anyone</p>",
                        From = "no-reply@gmail.com",
                        Title = "2FA Login",
                        Attachments = "",
                        SendDate = null
                    };
                    string? emailChannel = _configuration.GetValue<string>("EmailChannel");
                    if (emailChannel != null)
                    {
                        if (emailChannel == EmailChannelDefinitions.SmtpChannel)
                        {
                            isSent = isSent || await _smtpService.SendEmailAsync(emailFormModel.To, emailFormModel.Title, emailFormModel.Body, null, true);
                        }
                        else
                        {
                            isSent = isSent || false;
                        }
                    }
                    else
                    {
                        isSent = isSent || false;
                    }

                }

                if (isSent)
                {
                    await _context.Twofactors.AddAsync(twofactor);
                    await _context.SaveChangesAsync();
                    return twofactor;
                }

                return null;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) with the specified claims and session lifetime.
        /// </summary>
        /// <param name="claims">A list of claims to be included in the token.</param>
        /// <param name="sessionLifeTime">The lifetime of the token in minutes. Default is 60 minutes.</param>
        /// <returns>A signed and encrypted JWT as a string.</returns>
        private string GenerateJwtToken(List<Claim> claims, int sessionLifeTime = 60)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key!));
            var SubKeybytes = Encoding.ASCII.GetBytes(_jwtSettings.Key!);
            var SubKey = new SymmetricSecurityKey(SubKeybytes.Take(16).ToArray());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(sessionLifeTime),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,

                EncryptingCredentials = new EncryptingCredentials(SubKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) with a short expiration time (5 minutes) for two-factor authentication.
        /// </summary>
        /// <param name="claims">A list of claims to be included in the token.</param>
        /// <returns>A signed and encrypted JWT as a string, valid for 5 minutes.</returns>
        private string GenerateJwtToken2fa(List<Claim> claims)
        {
            return GenerateJwtToken(claims, 5);
        }

        /// <summary>
        /// Generates a list of claims for a user during the login process, including user information and a unique stamp for two-factor authentication.
        /// </summary>
        /// <param name="user">The user object containing user details.</param>
        /// <param name="stamp">A unique identifier (stamp) for two-factor authentication.</param>
        /// <returns>A list of claims representing the user's data and session details.</returns>
        private List<Claim> GenerateUserClaimForLogin(User user, string stamp)
        {
            try
            {
                List<Claim> claims =
                [
                    new (ClaimTypes.UserData, "Gest"),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Name, user.LastName),
                    new (ClaimTypes.MobilePhone, user.PhoneNumber),
                    new (ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(5).ToString()),
                    new (ClaimTypes.Thumbprint, stamp)
                ];

                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    bs.Append(item.Type + item.Value);
                }
                claims.Add(new Claim(ClaimTypes.Hash, Shared.Tools.Utility.GenerateSha512Hash(bs.ToString())));

                return claims;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return [];
            }
        }

        /// <summary>
        /// Generates a list of claims for a user during the reset process, including user information and a unique stamp for session verification.
        /// </summary>
        /// <param name="user">The user object containing user details.</param>
        /// <param name="stamp">A unique identifier (stamp) for reset session verification.</param>
        /// <returns>A list of claims representing the user's data and session details for the reset process.</returns>
        private List<Claim> GenerateUserClaimForReset(User user, string stamp)
        {
            try
            {
                List<Claim> claims =
                [
                    new (ClaimTypes.UserData, "Reset"),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Name, user.LastName),
                    new (ClaimTypes.MobilePhone, user.PhoneNumber),
                    new (ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(5).ToString()),
                    new (ClaimTypes.Thumbprint, stamp)
                ];

                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    bs.Append(item.Type + item.Value);
                }
                claims.Add(new Claim(ClaimTypes.Hash, Shared.Tools.Utility.GenerateSha512Hash(bs.ToString())));

                return claims;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return [];
            }
        }

        /// <summary>
        /// Generates a list of claims for a user, including user information, roles, modules, permissions, and a unique hash for session validation.
        /// </summary>
        /// <param name="user">The user object containing user details.</param>
        /// <param name="sessionLifeTime">The lifetime of the session in minutes. Default is 60 minutes.</param>
        /// <returns>A list of claims representing the user's data and session details.</returns>
        private List<Claim> GenerateUserClaim(User user, int sessionLifeTime = 60)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new (ClaimTypes.UserData, "User"),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new ("Timezone", user?.TimeZone_Info?.Value ?? "UTC"),
                    new (ClaimTypes.Name, user!.LastName),
                    new (ClaimTypes.MobilePhone, user!.PhoneNumber),
                    new (ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(sessionLifeTime).ToString()),
                    new (ClaimTypes.Email, user!.Email)
                };

                // Add User language claim if available
                if (!string.IsNullOrEmpty(user?.Language?.Name))
                {
                    claims.Add(new Claim("Language", user.Language.Name));
                }

                claims.AddRange(user?.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role!.RoleName)));
                claims.AddRange(user.UserModules.Select(um => new Claim("UserModule", um.ModuleId.ToString())));

                var permissionCollection = user.UserFeaturePermissions
                    .Select(ufp => ufp.FeaturePermissionId.ToString())
                    .Concat(user.UserSecurityGroups.SelectMany(usg => usg.SecurityGroup!.GroupFeaturePermissions)
                        .Select(gfp => gfp.FeaturePermissionId.ToString()))
                    .Distinct();

                claims.AddRange(permissionCollection.Select(permission => new Claim("FeaturePermission", permission)));
                var hashInput = string.Join("", claims.Select(c => c.Type + c.Value));
                var hash = Shared.Tools.Utility.GenerateSha512Hash(hashInput);
                claims.Add(new Claim(ClaimTypes.Hash, hash));

                return claims;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return [];
            }
        }

        /// <summary>
        /// Validates the user's login session by verifying the provided claims, including user ID, thumbprint (stamp), and hash.
        /// </summary>
        /// <param name="claims">The list of claims to verify, including user ID, thumbprint, and hash.</param>
        /// <returns>
        /// Returns true if the claims are valid and the user's session is verified, otherwise false.
        /// </returns>
        public bool LoginUserSignature(List<Claim> claims)
        {
            try
            {
                if (claims == null) return false;
                var userIdClaim = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var stampClaim = claims.Where(x => x.Type == ClaimTypes.Thumbprint).FirstOrDefault();
                var CurrentHashClaim = claims.Where(x => x.Type == ClaimTypes.Hash).FirstOrDefault();
                if (claims == null || stampClaim == null || CurrentHashClaim == null) return false;
                if (string.IsNullOrEmpty(userIdClaim!.Value) || string.IsNullOrEmpty(stampClaim.Value) || string.IsNullOrEmpty(CurrentHashClaim.Value)) return false;
                long UserId = 0;

                try
                {
                    UserId = long.Parse(userIdClaim.Value);
                }
                catch
                {
                    return false;
                }

                var verification = _context.Twofactors.Where(x => x.IsActive && !x.IsUsed && !x.IsDeleted && x.UserId == UserId && x.Stamp == stampClaim.Value && x.RequestType == TwofactorRequestTypeDefinitions.LoginRequest).FirstOrDefault();
                if (verification == null) return false;
                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    if (item.Type != ClaimTypes.Hash && item.Type != "nbf" && item.Type != "exp" && item.Type != "iat" && item.Type != "iss" && item.Type != "aud")
                        bs.Append(item.Type + item.Value);
                }

                return CurrentHashClaim.Value == Shared.Tools.Utility.GenerateSha512Hash(bs.ToString());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Validates the user's signature by comparing the hash from claims with the recalculated hash 
        /// and verifying the signature stored in the database.
        /// </summary>
        /// <param name="claims">The list of claims containing user details and the hash to validate.</param>
        /// <returns>
        /// Returns true if the claims and database signature are valid and match, otherwise false.
        /// </returns>
        public async Task<bool> UserSignature(List<Claim>? claims)
        {
            try
            {
                if (claims == null) return false;
                var CurrentHash = claims.Where(x => x.Type == ClaimTypes.Hash).FirstOrDefault();
                var CurrentUserId = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                if (CurrentHash == null || CurrentUserId == null) return false;
                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    if (item.Type != ClaimTypes.Hash && item.Type != "nbf" && item.Type != "exp" && item.Type != "iat" && item.Type != "iss" && item.Type != "aud")
                        bs.Append(item.Type + item.Value);
                }
                var calculated = Shared.Tools.Utility.GenerateSha512Hash(bs.ToString());
                if (CurrentHash.Value != calculated) return false;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == long.Parse(CurrentUserId.Value));
                if (user == null) return false;
                return user.Signature == calculated;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Verifies the login attempt using a one-time password (OTP) and user claims. 
        /// If the OTP is valid, proceeds with the login process; otherwise, handles an invalid attempt.
        /// </summary>
        /// <param name="principal">The ClaimsPrincipal containing user information and claims.</param>
        /// <param name="otp">The one-time password provided for verification.</param>
        /// <param name="lang">The language used for localized messages.</param>
        /// <param name="timeZone">The user's time zone for token expiration calculation.</param>
        /// <returns>
        /// A LoginResponse object indicating the success or failure of the login attempt.
        /// If successful, includes user details and a JWT token.
        /// </returns>
        public async Task<LoginResponse?> VerifyLogin(ClaimsPrincipal principal, string otp, string lang, string timeZone)
        {
            try
            {
                var UserId = principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var stamp = principal.Claims.Where(x => x.Type == ClaimTypes.Thumbprint).FirstOrDefault();
                if (UserId != null && stamp != null)
                {
                    var currentUser = await GetUserDataById(long.Parse(UserId.Value));
                    if (currentUser != null)
                    {
                        var verification = await _context.Twofactors.Where(x => x.UserId == currentUser.Id && x.IsSent && !x.IsUsed && x.IsActive && !x.IsDeleted && x.Code == otp && x.Stamp == stamp.Value && x.ExpirationDate > DateTime.UtcNow && x.RequestType == TwofactorRequestTypeDefinitions.LoginRequest).OrderByDescending(x => x.Id).AsTracking().FirstOrDefaultAsync();
                        if (verification != null)
                        {
                            verification.IsUsed = true;
                            verification.IsActive = false;
                            verification.ModifiedDate = DateTime.UtcNow;
                            verification.ModifiedBy = "System";
                            _context.Twofactors.Update(verification);
                            await _context.SaveChangesAsync();
                            return await LoginProcess(lang, currentUser, timeZone);
                        }
                        else
                        {
                            await HandleInvalidPasswordAsync(currentUser, lang);
                        }

                    }
                }
                return new LoginResponse() { MultiFactorRequired = false, Success = false, Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.invalid_otp_value, lang), Response = null, StatusCode = ResponseMessageCode.ErrorStatusCode, Token = null };
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return new LoginResponse() { MultiFactorRequired = false, Success = false, Message = exp.Message, Response = null, StatusCode = ResponseMessageCode.ErrorStatusCode, Token = null };
            }
        }

        /// <summary>
        /// Validates new user data, checking for conflicts or issues with specific fields such as email.
        /// </summary>
        /// <param name="userForm">The form containing user details to validate.</param>
        /// <param name="lang">The language used for localized validation messages.</param>
        /// <returns>
        /// A ValidationResult object containing any validation errors or success messages.
        /// </returns>
        public async Task<ValidationResult> VerifNewUserData(UserFormModel userForm, string lang)
        {
            var result = new ValidationResult();
            // Validate Email
            if (!string.IsNullOrEmpty(userForm.Email))
                result.Merge(await EmailExistAsync(userForm.Email, lang));

            return result;
        }

        /// <summary>
        /// Checks if an email address exists in the database and validates its format.
        /// </summary>
        /// <param name="email">The email address to validate and check for existence.</param>
        /// <param name="lang">The language used for localized validation messages.</param>
        /// <returns>
        /// A ValidationDetail object indicating whether the email is valid and available, 
        /// along with an appropriate message.
        /// </returns>
        public async Task<ValidationDetail> EmailExistAsync(string email, string lang)
        {
            if (!IsValidEmail(email))
            {
                return new ValidationDetail(false, _messageProvider.Find(MessageKeys.AccountServiceMessages.invalid_email_format, lang));
            }
            var exists = await _context.Users.AsNoTracking().AnyAsync(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            if (exists)
            {
                return new ValidationDetail(false, _messageProvider.Find(MessageKeys.AccountServiceMessages.email_exists, lang));
            }

            return new ValidationDetail(true);
        }

        /// <summary>
        /// Validates whether the provided email address is in a correct format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>
        /// A boolean value indicating whether the email address is valid.
        /// </returns>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Use IdnMapping class to convert Unicode domain names.
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Return true if strIn is in valid email format.
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Converts a Unicode domain name in an email address to its ASCII equivalent.
        /// </summary>
        /// <param name="match">A Regex match object containing the domain part of the email address.</param>
        /// <returns>
        /// The email address with the domain part converted to ASCII format.
        /// </returns>
        private string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new System.Globalization.IdnMapping();

            string domainName = idn.GetAscii(match.Groups[2].Value);
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        /// Validates a mobile number to ensure it meets basic criteria, such as length and non-null value.
        /// </summary>
        /// <param name="mobileNumber">The mobile number to validate.</param>
        /// <param name="lang">The language code for error messages.</param>
        /// <returns>
        /// A <see cref="ValidationDetail"/> object indicating whether the validation passed and an error message if it failed.
        /// </returns>
        private ValidationDetail ValidateMobileNumber(string mobileNumber, string lang)
        {
            if (string.IsNullOrEmpty(mobileNumber) || mobileNumber.Length < 8)
            {
                return new ValidationDetail(false, _messageProvider.Find(MessageKeys.invalid_mobile, lang));
            }
            return new ValidationDetail(true);
        }

        /// <summary>
        /// Verifies if the provided claims belong to an owner by validating against a predefined owner key and hash.
        /// </summary>
        /// <param name="claims">A list of claims to verify.</param>
        /// <returns>
        /// A boolean indicating whether the claims are verified as belonging to an owner.
        /// </returns>
        public bool VerifyOwner(List<Claim> claims)
        {
            try
            {
                if (claims == null)
                    return false;

                string? key = _configuration.GetValue<string>("OwnerKey");
                if (key == null)
                    return false;

                var CurrentHash = claims.Where(x => x.Type == ClaimTypes.Hash).FirstOrDefault();
                var PrimarySid = claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();

                if (CurrentHash == null || PrimarySid == null)
                    return false;

                if (PrimarySid.Value != Shared.Tools.Utility.GenerateSha512Hash(key))
                    return false;

                StringBuilder bs = new();
                List<Claim> FiltredClaims = [];
                foreach (var item in claims)
                {
                    if (item.Type != ClaimTypes.Hash && item.Type != "nbf" && item.Type != "exp" && item.Type != "iat" && item.Type != "iss" && item.Type != "aud")
                    {
                        bs.Append(item.Type + item.Value);
                        FiltredClaims.Add(item);
                    }
                }
                var calculated = Shared.Tools.Utility.GenerateSha512Hash(bs.ToString());

                return CurrentHash.Value == calculated;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Generates an owner access token based on the configured owner key and writes it to a file.
        /// </summary>
        /// <returns>
        /// A boolean indicating whether the token generation and file creation were successful.
        /// </returns>
        public async Task<bool> GenerateOwnerAccessToken()
        {
            try
            {
                string? key = _configuration.GetValue<string>("OwnerKey");
                if (key != null)
                {
                    string token = GenerateJwtToken(GenerateOwnerClaim(key, 10));
                    if (token != null)
                    {
                        DirectoryInfo directory = new("C:\\EyeToken");
                        if (!directory.Exists)
                        {
                            directory.Create();
                        }
                        await File.WriteAllTextAsync(directory.FullName + "\\EyeToken.txt", token);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Generates a list of claims for the application owner based on the provided key.
        /// </summary>
        /// <param name="key">The key used to generate the PrimarySid claim and ensure owner verification.</param>
        /// <param name="sessionLifeTime">The session lifetime for the generated token in minutes. Default is 10 minutes.</param>
        /// <returns>
        /// A list of claims representing the owner, including hashed verification claims.
        /// If an error occurs, returns an empty list.
        /// </returns>
        protected List<Claim> GenerateOwnerClaim(string key, int sessionLifeTime = 10)
        {
            try
            {
                List<Claim> claims =
                [
                    new (ClaimTypes.UserData, "Owner"),
                    new (ClaimTypes.NameIdentifier, "1"),
                    new (ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(sessionLifeTime).ToString()),
                    new (ClaimTypes.Role, RoleDefinitions.ApplicationOwner),
                    new (ClaimTypes.PrimarySid, Shared.Tools.Utility.GenerateSha512Hash(key)),
                ];

                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    bs.Append(item.Type + item.Value);
                }
                claims.Add(new Claim(ClaimTypes.Hash, Shared.Tools.Utility.GenerateSha512Hash(bs.ToString())));

                return claims;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return [];
            }
        }

        /// <summary>
        /// Resends a One-Time Password (OTP) for a login request.
        /// </summary>
        /// <param name="principal">The ClaimsPrincipal containing the user's claims.</param>
        /// <param name="lang">The language for localized messages.</param>
        /// <param name="timeZone">The user's time zone for token expiration.</param>
        /// <returns>
        /// A <see cref="LoginResponse"/> indicating whether the OTP was successfully resent or if an error occurred.
        /// If successful, a new token with the OTP is included in the response.
        /// </returns>
        public async Task<LoginResponse?> ResendOtp(ClaimsPrincipal principal, string lang, string timeZone)
        {
            try
            {
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var stampClaim = principal.FindFirst(ClaimTypes.Thumbprint)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(stampClaim))
                {
                    return CreateErrorResponse(MessageKeys.invalid_parameter, lang);
                }

                var currentUser = await GetUserDataById(long.Parse(userIdClaim));
                if (currentUser == null)
                {
                    return CreateErrorResponse(MessageKeys.invalid_parameter, lang);
                }

                if (currentUser.Lookout)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.account_locked, lang);
                }

                if (!currentUser.TwoFactorEnabled)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.two_factor_disabled, lang);
                }

                var verification = await _context.Twofactors
                    .Where(x => x.UserId == currentUser.Id && x.IsSent && !x.IsUsed && x.IsActive && !x.IsDeleted && x.Stamp == stampClaim && x.ExpirationDate > DateTime.UtcNow && x.RequestType == TwofactorRequestTypeDefinitions.LoginRequest)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();

                if (verification == null)
                {
                    return CreateErrorResponse(MessageKeys.invalid_parameter, lang);
                }

                if (!verification.CreatedDate.HasValue || verification.CreatedDate.Value.AddMinutes(2) > DateTime.UtcNow)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.otp_min_interval, lang);
                }

                var twoFA = await GenerateAndSendTwoFactorCodeAsync(currentUser);
                if (twoFA == null)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.otp_error, lang);
                }
                else
                {
                    return new LoginResponse
                    {
                        MultiFactorRequired = true,
                        Success = true,
                        Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.two_factor_required, lang),
                        Response = null,
                        StatusCode = ResponseMessageCode.SuccessStatusCode,
                        Token = GenerateJwtToken2fa(GenerateUserClaimForLogin(currentUser, twoFA.Stamp)),
                        TokenExpiration = Shared.Tools.Utility.ConvertToUserTimezone(DateTime.UtcNow.AddMinutes(5), timeZone)
                    };
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return CreateErrorResponse(MessageKeys.AccountServiceMessages.resend_otp_error, lang);
            }
        }

        /// <summary>
        /// Initiates a password reset request for a user by generating and sending a One-Time Password (OTP).
        /// </summary>
        /// <param name="email">The email address of the user requesting the password reset.</param>
        /// <param name="lang">The language for localized messages.</param>
        /// <param name="timeZone">The user's time zone for token expiration.</param>
        /// <returns>
        /// A <see cref="LoginResponse"/> indicating the success or failure of the password reset request.
        /// If successful, a new token with the OTP is included in the response.
        /// </returns>
        public async Task<LoginResponse> RequestPasswordReset(string email, string lang, string timeZone)
        {
            try
            {
                var currentUser = await GetUserDataByEmail(email);
                if (currentUser == null)
                {
                    return CreateErrorResponse(MessageKeys.invalid_parameter, lang);
                }

                if (currentUser.Lookout)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.account_locked, lang);
                }

                if (!currentUser.TwoFactorEnabled)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.two_factor_disabled, lang);
                }
                int resetPasswordInterval = _configuration.GetValue("ResetPasswordInterval", 10);
                var verification = await _context.Twofactors.
                    Where(x => x.UserId == currentUser.Id && !x.IsDeleted && x.RequestType == TwofactorRequestTypeDefinitions.ResetRequest)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
                if (verification != null)
                {
                    if (!verification.CreatedDate.HasValue || verification.CreatedDate.Value.AddMinutes(resetPasswordInterval) > DateTime.UtcNow)
                    {
                        return CreateErrorResponse(MessageKeys.reset_min_interval, lang);
                    }
                }

                var twoFA = await GenerateAndSendTwoFactorCodeForReset(currentUser);
                if (twoFA == null)
                {
                    return CreateErrorResponse(MessageKeys.AccountServiceMessages.otp_error, lang);
                }
                else
                {
                    return new LoginResponse
                    {
                        MultiFactorRequired = true,
                        Success = true,
                        Message = _messageProvider.Find(MessageKeys.AccountServiceMessages.two_factor_required, lang),
                        Response = null,
                        StatusCode = ResponseMessageCode.SuccessStatusCode,
                        Token = GenerateJwtToken2fa(GenerateUserClaimForReset(currentUser, twoFA.Stamp)),
                        TokenExpiration = Shared.Tools.Utility.ConvertToUserTimezone(DateTime.UtcNow.AddMinutes(5), timeZone)
                    };
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return CreateErrorResponse(MessageKeys.AccountServiceMessages.password_reset_error, lang);
            }
        }

        /// <summary>
        /// Resets the password for a user after validating the provided One-Time Password (OTP) and new password complexity.
        /// </summary>
        /// <param name="principal">The user's claims principal containing identifying information.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <param name="otp">The One-Time Password provided for verification.</param>
        /// <param name="lang">The language for localized messages.</param>
        /// <param name="timeZone">The user's time zone.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating the success or failure of the password reset process.
        /// If successful, returns true; otherwise, returns an error message.
        /// </returns>
        public async Task<GenericResponse<bool>> ResetPassword(ClaimsPrincipal principal, string newPassword, string otp, string lang, string timeZone)
        {
            try
            {
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var stampClaim = principal.FindFirst(ClaimTypes.Thumbprint)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(stampClaim))
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }
                // Validate the new password
                if (string.IsNullOrEmpty(newPassword) || !IsPasswordComplex(newPassword))
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.password_not_complex, lang);
                }
                var currentUser = await GetUserDataById(long.Parse(userIdClaim));
                if (currentUser == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                if (currentUser.Lookout)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.AccountServiceMessages.account_locked, lang);
                }

                var verification = await _context.Twofactors.
                    Where(x => x.UserId == currentUser.Id && !x.IsDeleted && x.RequestType == TwofactorRequestTypeDefinitions.ResetRequest && !x.IsUsed && x.IsActive && x.Code == otp && x.Stamp == stampClaim && x.ExpirationDate > DateTime.UtcNow)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();

                if (verification == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.AccountServiceMessages.otp_error, lang);

                verification.IsUsed = true;
                verification.IsActive = false;
                _context.Twofactors.Update(verification);
                currentUser.Signature = "*";
                currentUser.AccessFaildCount = 0;
                currentUser.LastPasswordUpdate = DateTime.UtcNow;
                currentUser.PasswordHash = Shared.Tools.Utility.GenerateSha512Hash(newPassword);
                _context.Users.Update(currentUser);
                await _context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.reset_faild, lang, exp);
            }
        }

        /// <summary>
        /// Checks if the provided password meets complexity requirements.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>
        /// True if the password is complex (contains at least 8 characters, 
        /// including an uppercase letter, a lowercase letter, a number, and a special character); otherwise, false.
        /// </returns>
        private bool IsPasswordComplex(string password)
        {
            if (password.Length < 8) return false;

            bool hasUpperChar = false;
            bool hasLowerChar = false;
            bool hasNumber = false;
            bool hasSpecialChar = false;

            foreach (var ch in password)
            {
                if (char.IsUpper(ch)) hasUpperChar = true;
                else if (char.IsLower(ch)) hasLowerChar = true;
                else if (char.IsDigit(ch)) hasNumber = true;
                else if (!char.IsLetterOrDigit(ch)) hasSpecialChar = true;
            }

            return hasUpperChar && hasLowerChar && hasNumber && hasSpecialChar;
        }

        /// <summary>
        /// Verifies the validity of claims for resetting a password.
        /// </summary>
        /// <param name="claims">The list of claims to validate.</param>
        /// <returns>
        /// True if the claims are valid and match the stored reset request data; otherwise, false.
        /// </returns>
        public bool VerifyResetPwd(List<Claim> claims)
        {
            try
            {
                var userIdClaim = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var stampClaim = claims.Where(x => x.Type == ClaimTypes.Thumbprint).FirstOrDefault();
                var CurrentHashClaim = claims.Where(x => x.Type == ClaimTypes.Hash).FirstOrDefault();
                if (claims == null || stampClaim == null || CurrentHashClaim == null) return false;
                if (string.IsNullOrEmpty(userIdClaim!.Value) || string.IsNullOrEmpty(stampClaim.Value) || string.IsNullOrEmpty(CurrentHashClaim.Value)) return false;
                long UserId = 0;
                try
                {
                    UserId = long.Parse(userIdClaim.Value);
                }
                catch
                {
                    return false;
                }
                var verification = _context.Twofactors.Where(x => x.IsActive && !x.IsUsed && !x.IsDeleted && x.UserId == UserId && x.Stamp == stampClaim.Value && x.RequestType == TwofactorRequestTypeDefinitions.ResetRequest).FirstOrDefault();
                if (verification == null) return false;
                StringBuilder bs = new();
                foreach (var item in claims)
                {
                    if (item.Type != ClaimTypes.Hash && item.Type != "nbf" && item.Type != "exp" && item.Type != "iat" && item.Type != "iss" && item.Type != "aud")
                        bs.Append(item.Type + item.Value);
                }

                return CurrentHashClaim.Value == Shared.Tools.Utility.GenerateSha512Hash(bs.ToString());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Creates a standardized error response for login-related operations.
        /// </summary>
        /// <param name="messageKey">The key for the error message to retrieve from the message provider.</param>
        /// <param name="lang">The language code for the localized message.</param>
        /// <returns>
        /// A <see cref="LoginResponse"/> object containing the error details, including the message and status.
        /// </returns>
        private LoginResponse CreateErrorResponse(string messageKey, string lang)
        {
            return new LoginResponse
            {
                MultiFactorRequired = false,
                Success = false,
                Message = _messageProvider.Find(messageKey, lang),
                Response = null,
                StatusCode = ResponseMessageCode.ErrorStatusCode
            };
        }
    }
}
