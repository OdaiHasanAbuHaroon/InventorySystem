using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class GroupFeaturePermissionMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<GroupFeaturePermissionMapper> _logger = loggerFactory.CreateLogger<GroupFeaturePermissionMapper>();

        public GroupFeaturePermissionDataModel MapEntityToDataModel(GroupFeaturePermission source)
        {
            GroupFeaturePermissionDataModel target = new();

            if (source.SecurityGroup != null)
            {
                SecurityGroupMapper securityGroupMapper = new(_httpContextDataProvider, _loggerFactory);
                target.SecurityGroup = securityGroupMapper.MapEntityToDataModel(source.SecurityGroup);
            }
            else
            {
                target.SecurityGroup = null;
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

            target.SecurityGroupId = source.SecurityGroupId;
            target.FeaturePermissionId = source.FeaturePermissionId;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<GroupFeaturePermissionDataModel> MapCollectionToDataModel(List<GroupFeaturePermission> collection)
        {
            List<GroupFeaturePermissionDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
