using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.Tools;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class TwofactorDataModel : DataModel
    {
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        public UserDataModel? User { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("ExpirationDate")]
        [JsonProperty("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [JsonPropertyName("IsUsed")]
        [JsonProperty("IsUsed")]
        public bool? IsUsed { get; set; } = false;

        [JsonPropertyName("IsSent")]
        [JsonProperty("IsSent")]
        public bool? IsSent { get; set; } = false;

        [JsonPropertyName("Stamp")]
        [JsonProperty("Stamp")]
        public string? Stamp { get; set; }

        [JsonPropertyName("RequestType")]
        [JsonProperty("RequestType")]
        public int? RequestType { get; set; }

        public override void FormatDates(string timeZone)
        {
            base.FormatDates(timeZone);
            if (ExpirationDate != null)
                ExpirationDate = Utility.ConvertToUserTimezone(ExpirationDate.Value, timeZone);
        }
    }
}
