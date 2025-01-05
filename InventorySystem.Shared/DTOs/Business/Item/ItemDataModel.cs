using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class ItemDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("UnitOfMeasurement")]
        [JsonProperty("UnitOfMeasurement")]
        public string? UnitOfMeasurement { get; set; }

        [JsonPropertyName("Serialized")]
        [JsonProperty("Serialized")]
        public bool? Serialized { get; set; }

        [JsonPropertyName("CategoryId")]
        [JsonProperty("CategoryId")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("Category")]
        [JsonProperty("Category")]
        [System.Text.Json.Serialization.JsonIgnore]
        public CategoryDataModel? Category { get; set; }

        [JsonPropertyName("BrandId")]
        [JsonProperty("BrandId")]
        public long BrandId { get; set; }

        [JsonPropertyName("Brand")]
        [JsonProperty("Brand")]
        [System.Text.Json.Serialization.JsonIgnore]
        public BrandDataModel? Brand { get; set; }

        [JsonPropertyName("ItemStatusId")]
        [JsonProperty("ItemStatusId")]
        public long ItemStatusId { get; set; }

        [JsonPropertyName("ItemStatus")]
        [JsonProperty("ItemStatus")]
        [System.Text.Json.Serialization.JsonIgnore]
        public ItemStatusDataModel? ItemStatus { get; set; }

        [JsonPropertyName("LocationId")]
        [JsonProperty("LocationId")]
        public long LocationId { get; set; }

        [JsonPropertyName("Location")]
        [JsonProperty("Location")]
        [System.Text.Json.Serialization.JsonIgnore]
        public LocationDataModel? Location { get; set; }
    }
}
