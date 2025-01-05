using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs
{
    [Serializable]
    public class LoginFormData
    {
        [JsonPropertyName("Email")]
        [JsonProperty("Email")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [JsonPropertyName("Password")]
        [JsonProperty("Password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
