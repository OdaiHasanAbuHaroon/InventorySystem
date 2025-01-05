using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class TimeZoneInformationFormModel : FormModel
    {
        [JsonPropertyName("Value")]
        [JsonProperty("Value")]
        [Required(ErrorMessage = "Zone information value is required")]
        [StringLength(100, ErrorMessage = "Zone information value cannot exceed 100 characters.")]
        public required string Value { get; set; }

        [JsonPropertyName("DisplayName")]
        [JsonProperty("DisplayName")]
        [Required(ErrorMessage = "Display name is required")]
        [StringLength(100, ErrorMessage = "Display name cannot exceed 100 characters.")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("StandardName")]
        [JsonProperty("StandardName")]
        [Required(ErrorMessage = "Standard name is required")]
        [StringLength(100, ErrorMessage = "Standard name cannot exceed 100 characters.")]
        public required string StandardName { get; set; }

        [JsonPropertyName("DaylightName")]
        [JsonProperty("DaylightName")]
        [Required(ErrorMessage = "Daylight name is required")]
        [StringLength(100, ErrorMessage = "Daylight name cannot exceed 100 characters.")]
        public required string DaylightName { get; set; }

        [JsonPropertyName("BaseUtcOffset")]
        [JsonProperty("BaseUtcOffset")]
        [Required(ErrorMessage = "Base Utc offset is required")]
        public required TimeSpan BaseUtcOffset { get; set; }

        [JsonPropertyName("SupportsDaylightSavingTime")]
        [JsonProperty("SupportsDaylightSavingTime")]
        [Required(ErrorMessage = "Supports daylight saving time is required")]
        public required bool SupportsDaylightSavingTime { get; set; }

        [JsonPropertyName("UtcOffset")]
        [JsonProperty("UtcOffset")]
        [Required(ErrorMessage = "Utc offset is required")]
        public required int UtcOffset { get; set; }
    }
}
