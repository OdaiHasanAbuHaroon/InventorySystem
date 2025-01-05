using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.BaseDTOs
{
    [Serializable]
    public abstract class FormModel
    {
        [JsonPropertyName("Id")]
        [JsonProperty("Id")]
        public long? Id { get; set; } = 0;
    }
}
