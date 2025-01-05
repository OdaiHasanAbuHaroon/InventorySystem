namespace InventorySystem.Shared.Messages
{
    public static partial class MessageKeys
    {
        public static class AccountServiceMessages
        {
            public readonly static string email_not_found = "email_not_found";

            public readonly static string account_locked = "account_locked";

            public readonly static string two_factor_required = "two_factor_required";

            public readonly static string otp_error = "otp_error";

            public readonly static string login_error = "login_error";

            public readonly static string login_success = "login_success";

            public readonly static string invalid_otp_value = "invalid_otp_value";

            public readonly static string invalid_email_format = "invalid_email_format";

            public readonly static string email_exists = "email_exists";

            public readonly static string two_factor_disabled = "two_factor_disabled";

            public readonly static string otp_min_interval = "otp_min_interval";

            public readonly static string resend_otp_error = "resend_otp_error";

            public readonly static string password_reset_error = "password_reset_error";
        }
    }
}