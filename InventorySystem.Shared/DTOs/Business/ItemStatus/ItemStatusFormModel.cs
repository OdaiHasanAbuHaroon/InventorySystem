using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class ItemStatusFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }
    }
}
