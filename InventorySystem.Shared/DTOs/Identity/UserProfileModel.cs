using InventorySystem.Shared.DTOs.Business;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserProfileModel
    {
        [JsonPropertyName("Id")]
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Email")]
        [JsonProperty("Email")]
        public string? Email { get; set; }

        [JsonPropertyName("FirstName")]
        [JsonProperty("FirstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("MiddleName")]
        [JsonProperty("MiddleName")]
        public string? MiddleName { get; set; }

        [JsonPropertyName("LastName")]
        [JsonProperty("LastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("PhoneNumber")]
        [JsonProperty("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("SmsEnabled")]
        [JsonProperty("SmsEnabled")]
        public bool SmsEnabled { get; set; } = false;

        [JsonPropertyName("ImageId")]
        [JsonProperty("ImageId")]
        public long? ImageId { get; set; }

        [JsonPropertyName("Image")]
        [JsonProperty("Image")]
        public AttachmentDataModel? Image { get; set; }

        [JsonPropertyName("ImageLink")]
        [JsonProperty("ImageLink")]
        public string? ImageLink { get; set; }

        [JsonPropertyName("Modules")]
        [JsonProperty("Modules")]
        [MapperIgnore]
        public List<ModuleDataModel> Modules { get; set; } = [];

        [JsonPropertyName("Roles")]
        [JsonProperty("Roles")]
        public List<string> Roles { get; set; } = [];
    }
}
