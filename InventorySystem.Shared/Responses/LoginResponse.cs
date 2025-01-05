using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Responses
{
    public class LoginResponse
    {
        [JsonPropertyName("Success")]
        [JsonProperty("Success")]
        public required bool Success { get; set; }

        [JsonPropertyName("Message")]
        [JsonProperty("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Response")]
        [JsonProperty("Response")]
        public UserProfileModel? Response { get; set; }

        [JsonPropertyName("Token")]
        [JsonProperty("Token")]
        public string? Token { get; set; }

        [JsonPropertyName("TokenExpiration")]
        [JsonProperty("TokenExpiration")]
        public DateTime TokenExpiration { get; set; }

        [JsonPropertyName("MultiFactorRequired")]
        [JsonProperty("MultiFactorRequired")]
        public required bool MultiFactorRequired { get; set; } = false;

        [JsonPropertyName("StatusCode")]
        [JsonProperty("StatusCode")]
        public string? StatusCode { get; set; }
    }
}
