using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserExtraInfo
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
    }
}
