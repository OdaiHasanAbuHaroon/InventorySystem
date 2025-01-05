using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class PersonFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [JsonPropertyName("Reference")]
        [JsonProperty("Reference")]
        [Required(ErrorMessage = "Reference is required.")]
        public required string Reference { get; set; }

        [JsonPropertyName("Type")]
        [JsonProperty("Type")]
        [Required(ErrorMessage = "Type is required.")]
        public required string Type { get; set; }

        [JsonPropertyName("Cost")]
        [JsonProperty("Cost")]
        [Required(ErrorMessage = "Cost is required.")]
        public required decimal Cost { get; set; }

        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        [Required(ErrorMessage = "UserId is required.")]
        public required long UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        [System.Text.Json.Serialization.JsonIgnore]
        public UserFormModel? User { get; set; }
    }
}
