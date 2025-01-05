using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class TimeZoneInformationDataModel : DataModel
    {
        [JsonPropertyName("Value")]
        [JsonProperty("Value")]
        public string? Value { get; set; }

        [JsonPropertyName("DisplayName")]
        [JsonProperty("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("StandardName")]
        [JsonProperty("StandardName")]
        public string? StandardName { get; set; }

        [JsonPropertyName("DaylightName")]
        [JsonProperty("DaylightName")]
        public string? DaylightName { get; set; }

        [JsonPropertyName("BaseUtcOffset")]
        [JsonProperty("BaseUtcOffset")]
        public TimeSpan? BaseUtcOffset { get; set; }

        [JsonPropertyName("SupportsDaylightSavingTime")]
        [JsonProperty("SupportsDaylightSavingTime")]
        public bool? SupportsDaylightSavingTime { get; set; }

        [JsonPropertyName("UtcOffset")]
        [JsonProperty("UtcOffset")]
        public int? UtcOffset { get; set; }

        [JsonPropertyName("Users")]
        [JsonProperty("Users")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserDataModel> Users { get; set; } = [];
    }
}
