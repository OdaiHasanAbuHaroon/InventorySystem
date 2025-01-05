using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class FeaturePermissionMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<FeaturePermissionMapper> _logger = loggerFactory.CreateLogger<FeaturePermissionMapper>();

        public FeaturePermissionDataModel MapEntityToDataModel(FeaturePermission source)
        {
            FeaturePermissionDataModel target = new();
            if (source.Feature != null)
            {
                FeatureMapper featureMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Feature = featureMapper.MapEntityToDataModel(source.Feature);
            }
            else
            {
                target.Feature = null;
            }

            if (source.Permission != null)
            {
                PermissionMapper permissionMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Permission = permissionMapper.MapEntityToDataModel(source.Permission);
            }
            else
            {
                target.Permission = null;
            }

            target.Id = source.Id;
            target.FeatureId = source.FeatureId;
            target.PermissionId = source.PermissionId;
            return target;
        }

        public List<FeaturePermissionDataModel> MapCollectionToDataModel(List<FeaturePermission> collection)
        {
            List<FeaturePermissionDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
