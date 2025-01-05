using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Core;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class SupplierFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [JsonPropertyName("ContactEmail")]
        [JsonProperty("ContactEmail")]
        public string? ContactEmail { get; set; }

        [JsonPropertyName("ContactName")]
        [JsonProperty("ContactName")]
        public string? ContactName { get; set; }

        [JsonPropertyName("ContactNumber")]
        [JsonProperty("ContactNumber")]
        public string? ContactNumber { get; set; }

        [JsonPropertyName("Address")]
        [JsonProperty("Address")]
        public string? Address { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("CountryId")]
        [JsonProperty("CountryId")]
        public long? CountryId { get; set; }

        [JsonPropertyName("Country")]
        [JsonProperty("Country")]
        [System.Text.Json.Serialization.JsonIgnore]
        public CountryFormModel? Country { get; set; }
    }
}
