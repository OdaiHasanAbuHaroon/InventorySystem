using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class SerialNumberMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<SerialNumberMapper> _logger = loggerFactory.CreateLogger<SerialNumberMapper>();

        public SerialNumberDataModel MapEntityToDataModel(SerialNumber source)
        {
            if (source == null)
                return new();

            SerialNumberDataModel target = new();

            // Map navigation properties
            if (source.Item != null)
            {
                ItemMapper itemMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Item = itemMapper.MapEntityToDataModel(source.Item);
            }

            // Map regular properties
            target.Serial = source.Serial;
            target.ItemId = source.ItemId;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public SerialNumber? MapDataFormToEntity(SerialNumberFormModel form)
        {
            if (form == null)
                return null;

            SerialNumber target = new()
            {
                Serial = form.Serial,
                ItemId = form.ItemId,
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public SerialNumberFormModel MapEntityToDataForm(SerialNumber source)
        {
            SerialNumberFormModel target = new()
            {
                Id = source.Id,
                Serial = source.Serial,
                ItemId = source.ItemId,
            };

            return target;
        }

        public List<SerialNumberDataModel> MapCollectionToDataModel(List<SerialNumber> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public SerialNumber? MapUpdateDataFormToEntity(SerialNumberFormModel form, SerialNumber current)
        {
            if (form == null || current == null)
                return current;

            current.Serial = form.Serial;
            current.ItemId = form.ItemId;

            return current;
        }
    }
}
