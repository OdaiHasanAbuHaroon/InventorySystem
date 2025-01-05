using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Entities.Core;

namespace InventorySystem.Shared.Entities.Configuration.Identity
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }

        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public required string PasswordHash { get; set; }

        public required string PhoneNumber { get; set; }

        public required bool TwoFactorEnabled { get; set; } = false;

        public required int AccessFaildCount { get; set; } = 0;

        public required bool LookoutEnabled { get; set; } = true;

        public required bool Lookout { get; set; } = false;

        public required bool EmailConfirmed { get; set; } = false;

        public required bool MobileNumberConfirmed { get; set; } = false;

        public required bool SmsEnabled { get; set; } = false;

        public string? Signature { get; set; }

        public string? Configuration { get; set; }

        public required string Gender { get; set; }

        public string? Address { get; set; }

        public int? UserFontSize { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public DateTime? LastPasswordUpdate { get; set; }

        public DateTime? LookoutEnd { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public required long TimeZone_InfoId { get; set; }
        public virtual TimeZoneInformation? TimeZone_Info { get; set; }

        public required long LanguageId { get; set; }
        public virtual Language? Language { get; set; }

        public required long ThemeId { get; set; }
        public virtual Theme? Theme { get; set; }

        public long? ImageId { get; set; }
        public virtual Attachment? Image { get; set; }

        #region Collections

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public virtual ICollection<Twofactor> Twofactors { get; set; } = new HashSet<Twofactor>();

        public ICollection<UserModule> UserModules { get; set; } = new HashSet<UserModule>();

        public ICollection<UserFeaturePermission> UserFeaturePermissions { get; set; } = new HashSet<UserFeaturePermission>();

        public ICollection<UserSecurityGroup> UserSecurityGroups { get; set; } = new HashSet<UserSecurityGroup>();

        #endregion
    }
}
