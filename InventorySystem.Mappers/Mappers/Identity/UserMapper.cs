using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Tools;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class UserMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<UserMapper> _logger = loggerFactory.CreateLogger<UserMapper>();

        public UserFormModel MapIFormModelTFormModel(UserFormModel form, User user)
        {
            UserFormModel NewItem = new()
            {
                DateOfBirth = form.DateOfBirth,
                Gender = form.Gender,
                LanguageId = form.LanguageId,
                ThemeId = form.ThemeId,
                TimeZone_InfoId = form.TimeZone_InfoId,
                Address = form.Address,
                UserFontSize = form.UserFontSize,
                Email = form.Email,
                EmailConfirmed = form.EmailConfirmed,
                FirstName = form.FirstName,
                LastName = form.LastName,
                LookoutEnabled = user.LookoutEnabled,
                MobileNumberConfirmed = form.MobileNumberConfirmed,
                PhoneNumber = form.PhoneNumber,
                SmsEnabled = form.SmsEnabled,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Id = user.Id,
                MiddleName = form.MiddleName,
                UserExtraInfo = form.UserExtraInfo
            };

            if (form.Image != null)
            {
                NewItem.Image = new AttachmentFormModel()
                {
                    Extention = form.Image.Extention,
                    FileContent = form.Image.FileContent,
                    Name = form.Image.Name,
                    Id = form.Image.Id,
                    Path = form.Image.Path
                };
            }

            return NewItem;
        }

        public UserDataModel MapEntityToDataModel(User source)
        {
            UserDataModel target = new();

            if (source.Image != null)
            {
                AttachmentMapper attachmentMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Image = attachmentMapper.MapEntityToDataModel(source.Image);
            }
            else
            {
                target.Image = null;
            }

            target.Email = source.Email;
            target.FirstName = source.FirstName;
            target.MiddleName = source.MiddleName;
            target.LastName = source.LastName;
            target.PhoneNumber = source.PhoneNumber;
            target.EmailConfirmed = source.EmailConfirmed;
            target.MobileNumberConfirmed = source.MobileNumberConfirmed;
            target.SmsEnabled = source.SmsEnabled;
            target.ImageId = source.ImageId;
            target.Id = source.Id;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.ModifiedBy = source.ModifiedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<UserDataModel> MapCollectionToDataModel(List<User> collection)
        {
            List<UserDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }

        public User MapUpdateDataFormToEntity(UserFormModel form, User current)
        {
            current.MiddleName = form.MiddleName;
            current.Email = form.Email;
            current.EmailConfirmed = form.EmailConfirmed;
            current.FirstName = form.FirstName;
            current.LastName = form.LastName;
            current.MobileNumberConfirmed = form.MobileNumberConfirmed;
            current.PhoneNumber = form.PhoneNumber;
            current.SmsEnabled = form.SmsEnabled;

            return current;
        }

        public UserExtraInfo? MapUserExtraInfo(User child)
        {
            UserExtraInfo result = new()
            {
                DateOfBirth = child.DateOfBirth,
                Gender = child.Gender,
                LanguageId = child.LanguageId,
                ThemeId = child.ThemeId,
                TimeZone_InfoId = child.TimeZone_InfoId,
                Address = child.Address,
                UserFontSize = child.UserFontSize
            };

            return result;
        }

        public User MapDataFormToEntity(UserFormModel form)
        {
            User user = new()
            {
                DateOfBirth = form.DateOfBirth,
                Gender = form.Gender,
                LanguageId = form.LanguageId,
                ThemeId = form.ThemeId,
                TimeZone_InfoId = form.TimeZone_InfoId,
                Address = form.Address,
                UserFontSize = form.UserFontSize,
                Email = form.Email,
                FirstName = form.FirstName,
                MiddleName = form.MiddleName,
                LastName = form.LastName,
                PasswordHash = Utility.GenerateSha512Hash(form.Password ?? "wYX%0<|HK09"),
                PhoneNumber = form.PhoneNumber,
                TwoFactorEnabled = form.TwoFactorEnabled,
                AccessFaildCount = 0,
                LookoutEnabled = form.LookoutEnabled,
                Lookout = false,
                EmailConfirmed = form.EmailConfirmed,
                MobileNumberConfirmed = form.MobileNumberConfirmed,
                SmsEnabled = form.SmsEnabled,
                Signature = "*",
                Configuration = "",
                LastPasswordUpdate = null,
                LookoutEnd = null,
                LastLoginDate = null,
                IsActive = true,
                IsDeleted = false,
            };

            return user;
        }

    }
}
