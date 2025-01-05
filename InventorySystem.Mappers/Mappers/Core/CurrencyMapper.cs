using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class CurrencyMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<CurrencyMapper> _logger = loggerFactory.CreateLogger<CurrencyMapper>();

        public CurrencyDataModel MapEntityToDataModel(Currency source)
        {
            CurrencyDataModel target = new()
            {
                Name = source.Name,
                Code = source.Code,
                Symbol = source.Symbol,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public Currency MapDataFormToEntity(CurrencyFormModel form)
        {
            Currency target = new()
            {
                Name = form.Name,
                Code = form.Code,
                Symbol = form.Symbol,
            };

            if (form.Id != null)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public CurrencyFormModel MapEntityToDataForm(Currency source)
        {
            CurrencyFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Code = source.Code,
                Symbol = source.Symbol,
            };

            return target;
        }

        public List<CurrencyDataModel> MapCollectionToDataModel(List<Currency> collection)
        {
            List<CurrencyDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
