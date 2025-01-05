using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class UserFeaturePermissionMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<UserFeaturePermissionMapper> _logger = loggerFactory.CreateLogger<UserFeaturePermissionMapper>();

        public UserFeaturePermissionDataModel MapEntityToDataModel(UserFeaturePermission source)
        {
            UserFeaturePermissionDataModel target = new();

            if (source.User != null)
            {
                UserMapper userMapper = new(_httpContextDataProvider, _loggerFactory);
                target.User = userMapper.MapEntityToDataModel(source.User);
            }
            else
            {
                target.User = null;
            }

            if (source.FeaturePermission != null)
            {
                FeaturePermissionMapper featurePermissionMapper = new(_httpContextDataProvider, _loggerFactory);
                target.FeaturePermission = featurePermissionMapper.MapEntityToDataModel(source.FeaturePermission);
            }
            else
            {
                target.FeaturePermission = null;
            }

            target.UserId = source.UserId;
            target.FeaturePermissionId = source.FeaturePermissionId;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<UserFeaturePermissionDataModel> MapCollectionToDataModel(List<UserFeaturePermission> collection)
        {
            List<UserFeaturePermissionDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }

    }
}
