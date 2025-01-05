using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class LocationFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [JsonPropertyName("Type")]
        [JsonProperty("Type")]
        [Required(ErrorMessage = "Type is required.")]
        public required string Type { get; set; }

        [JsonPropertyName("Address")]
        [JsonProperty("Address")]
        public string? Address { get; set; }

        [JsonPropertyName("ParentLocationId")]
        [JsonProperty("ParentLocationId")]
        public long? ParentLocationId { get; set; }
    }
}
