using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class CountryMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<CountryMapper> _logger = loggerFactory.CreateLogger<CountryMapper>();

        public CountryDataModel MapEntityToDataModel(Country source)
        {
            CountryDataModel target = new()
            {
                Name = source.Name,
                Code = source.Code,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public CountryFormModel MapEntityToDataForm(Country source)
        {
            CountryFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Code = source.Code
            };

            return target;
        }

        public Country MapDataFormToEntity(CountryFormModel form)
        {
            Country target = new()
            {
                Name = form.Name,
                Code = form.Code,
            };

            if (form.Id != null)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public List<CountryDataModel> MapCollectionToDataModel(List<Country> collection)
        {
            List<CountryDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
