using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class ThemeMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<ThemeMapper> _logger = loggerFactory.CreateLogger<ThemeMapper>();

        public ThemeDataModel MapEntityToDataModel(Theme source)
        {
            ThemeDataModel target = new()
            {
                Name = source.Name,
                Color = source.Color,
                FontSize = source.FontSize,
                IsDefault = source.IsDefault,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<ThemeDataModel> MapCollectionToDataModel(List<Theme> collection)
        {
            List<ThemeDataModel> result = [];

            foreach (var theme in collection)
            {
                result.Add(MapEntityToDataModel(theme));
            }

            return result;
        }
    }
}
