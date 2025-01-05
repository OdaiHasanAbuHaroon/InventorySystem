using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class ItemFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [JsonPropertyName("UnitOfMeasurement")]
        [JsonProperty("UnitOfMeasurement")]
        [Required(ErrorMessage = "UnitOfMeasurement is required.")]
        public required string UnitOfMeasurement { get; set; }

        [JsonPropertyName("Serialized")]
        [JsonProperty("Serialized")]
        [Required(ErrorMessage = "Serialized is required.")]
        public required bool Serialized { get; set; }

        [JsonPropertyName("CategoryId")]
        [JsonProperty("CategoryId")]
        [Required(ErrorMessage = "CategoryId is required.")]
        public required long CategoryId { get; set; }

        [JsonPropertyName("Category")]
        [JsonProperty("Category")]
        [System.Text.Json.Serialization.JsonIgnore]
        public CategoryFormModel? Category { get; set; }

        [JsonPropertyName("BrandId")]
        [JsonProperty("BrandId")]
        [Required(ErrorMessage = "BrandId is required.")]
        public required long BrandId { get; set; }

        [JsonPropertyName("Brand")]
        [JsonProperty("Brand")]
        [System.Text.Json.Serialization.JsonIgnore]
        public BrandFormModel? Brand { get; set; }

        [JsonPropertyName("ItemStatusId")]
        [JsonProperty("ItemStatusId")]
        [Required(ErrorMessage = "ItemStatusId is required.")]
        public required long ItemStatusId { get; set; }

        [JsonPropertyName("ItemStatus")]
        [JsonProperty("ItemStatus")]
        [System.Text.Json.Serialization.JsonIgnore]
        public ItemStatusFormModel? ItemStatus { get; set; }

        [JsonPropertyName("LocationId")]
        [JsonProperty("LocationId")]
        [Required(ErrorMessage = "LocationId is required.")]
        public required long LocationId { get; set; }

        [JsonPropertyName("Location")]
        [JsonProperty("Location")]
        [System.Text.Json.Serialization.JsonIgnore]
        public LocationFormModel? Location { get; set; }
    }
}
