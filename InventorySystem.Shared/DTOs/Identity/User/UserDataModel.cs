using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Tools;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserDataModel : DataModel
    {
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

        [JsonPropertyName("PasswordHash")]
        [JsonProperty("PasswordHash")]
        [MapperIgnore]
        public string? PasswordHash { get; set; }

        [JsonPropertyName("PhoneNumber")]
        [JsonProperty("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("TwoFactorEnabled")]
        [JsonProperty("TwoFactorEnabled")]
        [MapperIgnore]
        public bool? TwoFactorEnabled { get; set; } = false;

        [JsonPropertyName("AccessFaildCount")]
        [JsonProperty("AccessFaildCount")]
        [MapperIgnore]
        public int? AccessFaildCount { get; set; } = 0;

        [JsonPropertyName("LookoutEnabled")]
        [JsonProperty("LookoutEnabled")]
        [MapperIgnore]
        public bool? LookoutEnabled { get; set; } = true;

        [JsonPropertyName("Lookout")]
        [JsonProperty("Lookout")]
        [MapperIgnore]
        public bool? Lookout { get; set; } = false;

        [JsonPropertyName("EmailConfirmed")]
        [JsonProperty("EmailConfirmed")]
        public bool? EmailConfirmed { get; set; } = false;

        [JsonPropertyName("MobileNumberConfirmed")]
        [JsonProperty("MobileNumberConfirmed")]
        public bool? MobileNumberConfirmed { get; set; } = false;

        [JsonPropertyName("SmsEnabled")]
        [JsonProperty("SmsEnabled")]
        public bool? SmsEnabled { get; set; } = false;

        [JsonPropertyName("Signature")]
        [JsonProperty("Signature")]
        [MapperIgnore]
        public string? Signature { get; set; }

        [JsonPropertyName("Configuration")]
        [JsonProperty("Configuration")]
        [MapperIgnore]
        public string? Configuration { get; set; }

        [JsonPropertyName("LastPasswordUpdate")]
        [JsonProperty("LastPasswordUpdate")]
        [MapperIgnore]
        public DateTime? LastPasswordUpdate { get; set; }

        [JsonPropertyName("LookoutEnd")]
        [JsonProperty("LookoutEnd")]
        [MapperIgnore]
        public DateTime? LookoutEnd { get; set; }

        [JsonPropertyName("LastLoginDate")]
        [JsonProperty("LastLoginDate")]
        [MapperIgnore]
        public DateTime? LastLoginDate { get; set; }

        [JsonPropertyName("ImageId")]
        [JsonProperty("ImageId")]
        public long? ImageId { get; set; }

        [JsonPropertyName("Image")]
        [JsonProperty("Image")]
        public AttachmentDataModel? Image { get; set; }

        [JsonPropertyName("UserRoles")]
        [JsonProperty("UserRoles")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserRoleDataModel> UserRoles { get; set; } = [];

        [JsonPropertyName("Twofactors")]
        [JsonProperty("Twofactors")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<TwofactorDataModel> Twofactors { get; set; } = [];

        [JsonPropertyName("UserModules")]
        [JsonProperty("UserModules")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserModuleDataModel> UserModules { get; set; } = [];

        [JsonPropertyName("UserFeaturePermissions")]
        [JsonProperty("UserFeaturePermissions")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserFeaturePermissionDataModel> UserFeaturePermissions { get; set; } = [];

        [JsonPropertyName("UserSecurityGroups")]
        [JsonProperty("UserSecurityGroups")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserSecurityGroupDataModel> UserSecurityGroups { get; set; } = [];

        [JsonPropertyName("UserExtraInfo")]
        [JsonProperty("UserExtraInfo")]
        [MapperIgnore]
        public UserExtraInfo? UserExtraInfo { get; set; }

        public override void FormatDates(string timeZone)
        {
            base.FormatDates(timeZone);
            if (LastPasswordUpdate != null)
                LastPasswordUpdate = Utility.ConvertToUserTimezone(LastPasswordUpdate.Value, timeZone);
            if (LookoutEnd != null)
                LookoutEnd = Utility.ConvertToUserTimezone(LookoutEnd.Value, timeZone);
            if (LastLoginDate != null)
                LastLoginDate = Utility.ConvertToUserTimezone(LastLoginDate.Value, timeZone);
        }
    }
}
