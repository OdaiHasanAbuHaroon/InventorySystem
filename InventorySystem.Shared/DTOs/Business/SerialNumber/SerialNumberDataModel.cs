using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class SerialNumberDataModel : DataModel
    {
        [JsonPropertyName("Serial")]
        [JsonProperty("Serial")]
        public string? Serial { get; set; }

        [JsonPropertyName("ItemId")]
        [JsonProperty("ItemId")]
        public long ItemId { get; set; }

        [JsonPropertyName("Item")]
        [JsonProperty("Item")]
        [System.Text.Json.Serialization.JsonIgnore]
        public ItemDataModel? Item { get; set; }
    }
}
