using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class LanguageMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<LanguageMapper> _logger = loggerFactory.CreateLogger<LanguageMapper>();

        public LanguageDataModel MapEntityToDataModel(Language source)
        {
            LanguageDataModel target = new()
            {
                Name = source.Name,
                NativeName = source.NativeName,
                Code = source.Code,
                NameAr = source.NameAr,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<LanguageDataModel> MapCollectionToDataModel(List<Language> collection)
        {
            List<LanguageDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
