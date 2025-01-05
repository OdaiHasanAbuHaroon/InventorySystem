using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Business;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserFormModel : FormModel
    {
        [JsonPropertyName("DateOfBirth")]
        [JsonProperty("DateOfBirth")]
        [Required(ErrorMessage = "DateOfBirth is required.")]
        public required DateTime DateOfBirth { get; set; }

        [JsonPropertyName("Gender")]
        [JsonProperty("Gender")]
        [Required(ErrorMessage = "Gender is required.")]
        public required string Gender { get; set; }

        [JsonPropertyName("LanguageId")]
        [JsonProperty("LanguageId")]
        [Required(ErrorMessage = "LanguageId is required.")]
        public required long LanguageId { get; set; }

        [JsonPropertyName("ThemeId")]
        [JsonProperty("ThemeId")]
        [Required(ErrorMessage = "ThemeId is required.")]
        public required long ThemeId { get; set; }

        [JsonPropertyName("TimeZone_InfoId")]
        [JsonProperty("TimeZone_InfoId")]
        [Required(ErrorMessage = "TimeZone_InfoId is required.")]
        public required long TimeZone_InfoId { get; set; }

        [JsonPropertyName("Address")]
        [JsonProperty("Address")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [JsonPropertyName("UserFontSize")]
        [JsonProperty("UserFontSize")]
        public int? UserFontSize { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [JsonPropertyName("Email")]
        [JsonProperty("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [JsonPropertyName("FirstName")]
        [JsonProperty("FirstName")]
        [MaxLength(500, ErrorMessage = "First Name cannot exceed 500 characters.")]
        public required string FirstName { get; set; }

        [JsonPropertyName("MiddleName")]
        [JsonProperty("MiddleName")]
        [MaxLength(500, ErrorMessage = "Middle Name cannot exceed 500 characters.")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [JsonPropertyName("LastName")]
        [JsonProperty("LastName")]
        [MaxLength(500, ErrorMessage = "Last Name cannot exceed 500 characters.")]
        public required string LastName { get; set; }

        [JsonPropertyName("Password")]
        [JsonProperty("Password")]
        [MaxLength(200, ErrorMessage = "Password cannot exceed 200 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [JsonPropertyName("PhoneNumber")]
        [JsonProperty("PhoneNumber")]
        [MaxLength(50, ErrorMessage = "Phone Number cannot exceed 50 characters.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "TwoFactorEnabled is required.")]
        [JsonPropertyName("TwoFactorEnabled")]
        [JsonProperty("TwoFactorEnabled")]
        public required bool TwoFactorEnabled { get; set; } = false;

        [Required(ErrorMessage = "Lookout Enabled is required.")]
        [JsonPropertyName("LookoutEnabled")]
        [JsonProperty("LookoutEnabled")]
        public required bool LookoutEnabled { get; set; } = true;

        [Required(ErrorMessage = "Email Confirmed is required.")]
        [JsonPropertyName("EmailConfirmed")]
        [JsonProperty("EmailConfirmed")]
        public required bool EmailConfirmed { get; set; } = false;

        [Required(ErrorMessage = "Mobile Number Confirmed is required.")]
        [JsonPropertyName("MobileNumberConfirmed")]
        [JsonProperty("MobileNumberConfirmed")]
        public required bool MobileNumberConfirmed { get; set; } = false;

        [Required(ErrorMessage = "Sms Enabled is required.")]
        [JsonPropertyName("SmsEnabled")]
        [JsonProperty("SmsEnabled")]
        public required bool SmsEnabled { get; set; } = false;

        [JsonPropertyName("Image")]
        [JsonProperty("Image")]
        public AttachmentFormModel? Image { get; set; }

        [JsonPropertyName("UserExtraInfo")]
        [JsonProperty("UserExtraInfo")]
        public UserExtraInfo? UserExtraInfo { get; set; }

    }
}
