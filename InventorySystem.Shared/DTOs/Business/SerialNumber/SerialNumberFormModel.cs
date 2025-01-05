using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class SerialNumberFormModel : FormModel
    {
        [JsonPropertyName("Serial")]
        [JsonProperty("Serial")]
        [Required(ErrorMessage = "Serial is required.")]
        public required string Serial { get; set; }

        [JsonPropertyName("ItemId")]
        [JsonProperty("ItemId")]
        [Required(ErrorMessage = "ItemId is required.")]
        public required long ItemId { get; set; }

        [JsonPropertyName("Item")]
        [JsonProperty("Item")]
        [System.Text.Json.Serialization.JsonIgnore]
        public ItemFormModel? Item { get; set; }
    }
}
