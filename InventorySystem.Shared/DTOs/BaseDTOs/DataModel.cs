using InventorySystem.Shared.Tools;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.BaseDTOs
{
    [Serializable]
    public abstract class DataModel
    {
        [JsonPropertyName("Id")]
        [JsonProperty("Id")]
        public long? Id { get; set; }

        [JsonPropertyName("CreatedDate")]
        [JsonProperty("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("CreatedBy")]
        [JsonProperty("CreatedBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("ModifiedDate")]
        [JsonProperty("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("ModifiedBy")]
        [JsonProperty("ModifiedBy")]
        public string? ModifiedBy { get; set; }

        public virtual void FormatDates(string timeZone)
        {
            if (CreatedDate != null)
            {
                CreatedDate = Utility.ConvertToUserTimezone(CreatedDate.Value, timeZone);
            }
            if (ModifiedDate != null)
            {
                ModifiedDate = Utility.ConvertToUserTimezone(ModifiedDate.Value, timeZone);
            }
        }

    }
}
