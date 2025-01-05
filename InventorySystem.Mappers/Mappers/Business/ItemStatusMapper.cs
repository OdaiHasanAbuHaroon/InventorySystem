using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class ItemStatusMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<ItemStatusMapper> _logger = loggerFactory.CreateLogger<ItemStatusMapper>();

        public ItemStatusDataModel MapEntityToDataModel(ItemStatus source)
        {
            if (source == null)
                return new();

            ItemStatusDataModel target = new();

            // Map regular properties
            target.Name = source.Name;
            target.Description = source.Description;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public ItemStatus? MapDataFormToEntity(ItemStatusFormModel form)
        {
            if (form == null)
                return null;

            ItemStatus target = new()
            {
                Name = form.Name,
                Description = form.Description
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public ItemStatusFormModel MapEntityToDataForm(ItemStatus source)
        {
            ItemStatusFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
            };

            return target;
        }

        public List<ItemStatusDataModel> MapCollectionToDataModel(List<ItemStatus> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public ItemStatus? MapUpdateDataFormToEntity(ItemStatusFormModel form, ItemStatus current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Description = form.Description;

            return current;
        }
    }
}
