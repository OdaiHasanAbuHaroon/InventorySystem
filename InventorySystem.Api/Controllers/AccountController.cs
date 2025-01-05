using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers
{
    /// <summary>
    /// Controller for managing user account-related operations such as login, OTP verification, and password reset.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBaseExt
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="accountService">Service handling account-related business logic.</param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Authenticates the user with provided login credentials.
        /// </summary>
        /// <param name="loginForm">The user's login credentials.</param>
        /// <returns>A <see cref="LoginResponse"/> containing login details and status.</returns>
        [AllowAnonymous, HttpPost("Login")]
        public async Task<LoginResponse> Login([FromBody] LoginFormData loginForm)
        {
            return await _accountService.Login(loginForm, GetLanguage(), GetTimeZone());
        }

        /// <summary>
        /// Verifies the login by validating the OTP provided by the user.
        /// </summary>
        /// <param name="otp">The one-time password provided by the user.</param>
        /// <returns>A <see cref="LoginResponse"/> containing verification details if successful, otherwise null.</returns>
        [HttpPost("VerifyLogin")]
        public async Task<LoginResponse?> VerifyLogin(string otp)
        {
            return await _accountService.VerifyLogin(User, otp, GetLanguage(), GetTimeZone());
        }

        /// <summary>
        /// Resends the OTP to the authenticated user.
        /// </summary>
        /// <returns>A <see cref="LoginResponse"/> containing the status of the resend operation if successful, otherwise null.</returns>
        [HttpPost("ResendOtp")]
        public async Task<LoginResponse?> ResendOtp()
        {
            return await _accountService.ResendOtp(User, GetLanguage(), GetTimeZone());
        }

        /// <summary>
        /// Requests a password reset for a given email address.
        /// </summary>
        /// <param name="email">The email address of the user requesting a password reset.</param>
        /// <returns>A <see cref="LoginResponse"/> containing the status of the password reset request.</returns>
        [AllowAnonymous, HttpPost("RequestPasswordReset")]
        public async Task<LoginResponse> RequestPasswordReset(string email)
        {
            return await _accountService.RequestPasswordReset(email, GetLanguage(), GetTimeZone());
        }

        /// <summary>
        /// Resets the user's password using a new password and OTP for verification.
        /// </summary>
        /// <param name="newPassword">The new password to be set.</param>
        /// <param name="otp">The one-time password for verification.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> containing a boolean indicating the success of the operation.</returns>
        [HttpPost("ResetPassword")]
        public async Task<GenericResponse<bool>> ResetPassword(string newPassword, string otp)
        {
            return await _accountService.ResetPassword(User, newPassword, otp, GetLanguage(), GetTimeZone());
        }
    }
}
