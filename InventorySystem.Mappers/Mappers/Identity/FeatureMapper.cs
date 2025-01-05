using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class FeatureMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<FeatureMapper> _logger = loggerFactory.CreateLogger<FeatureMapper>();

        public FeatureDataModel MapEntityToDataModel(Feature source)
        {
            FeatureDataModel target = new();

            if (source.Module != null)
            {
                ModuleMapper moduleMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Module = moduleMapper.MapEntityToDataModel(source.Module);
            }
            else
            {
                target.Module = null;
            }

            target.ModuleId = source.ModuleId;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Id = source.Id;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.ModifiedBy = source.ModifiedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<FeatureDataModel> MapCollectionToDataModel(List<Feature> collection)
        {
            List<FeatureDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
