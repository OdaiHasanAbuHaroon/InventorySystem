using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.Responses;
using System.Security.Claims;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginFormData accountLoginForm, string lang, string timeZone);

        bool LoginUserSignature(List<Claim> claims);

        Task<bool> UserSignature(List<Claim>? claims);

        bool VerifyOwner(List<Claim> claims);

        bool VerifyResetPwd(List<Claim> claims);

        Task<bool> GenerateOwnerAccessToken();

        Task<LoginResponse?> VerifyLogin(ClaimsPrincipal principal, string otp, string lang, string timeZone);

        Task<LoginResponse?> ResendOtp(ClaimsPrincipal user, string lang, string timeZone);

        Task<LoginResponse> RequestPasswordReset(string email, string lang, string timeZone);

        Task<GenericResponse<bool>> ResetPassword(ClaimsPrincipal user, string newPassword, string otp, string lang, string timeZone);
    }
}
