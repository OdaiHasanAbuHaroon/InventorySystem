using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class TimeZoneInformationMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<TimeZoneInformationMapper> _logger = loggerFactory.CreateLogger<TimeZoneInformationMapper>();

        public TimeZoneInformationDataModel MapEntityToDataModel(TimeZoneInformation source)
        {
            TimeZoneInformationDataModel target = new()
            {
                Value = source.Value,
                DisplayName = source.DisplayName,
                StandardName = source.StandardName,
                DaylightName = source.DaylightName,
                BaseUtcOffset = source.BaseUtcOffset,
                SupportsDaylightSavingTime = source.SupportsDaylightSavingTime,
                UtcOffset = source.UtcOffset,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<TimeZoneInformationDataModel> MapCollectionToDataModel(List<TimeZoneInformation> collection)
        {
            List<TimeZoneInformationDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
