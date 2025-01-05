using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace InventorySystem.Shared.Responses
{
    public class GenericResponse<T>
    {
        [JsonPropertyName("Success")]
        [JsonProperty("Success")]
        public required bool Success { get; set; }

        [JsonPropertyName("Messages")]
        [JsonProperty("Messages")]
        public List<ResponseMessage> Messages { get; set; } = [];

        [JsonPropertyName("Response")]
        [JsonProperty("Response")]
        public T? Response { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("TotalRecords")]
        [JsonProperty("TotalRecords")]
        public long? TotalRecords { get; set; } = 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("Page")]
        [JsonProperty("Page")]
        public long? Page { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("PageSize")]
        [JsonProperty("PageSize")]
        public long? PageSize { get; set; }
    }
}
