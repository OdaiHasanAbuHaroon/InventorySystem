using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class LocationMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<LocationMapper> _logger = loggerFactory.CreateLogger<LocationMapper>();

        public LocationDataModel MapEntityToDataModel(Location source)
        {
            if (source == null)
                return new();

            LocationDataModel target = new();

            // Map regular properties
            target.Name = source.Name;
            target.Type = source.Type;
            target.Address = source.Address;
            target.ParentLocationId = source.ParentLocationId;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Location? MapDataFormToEntity(LocationFormModel form)
        {
            if (form == null)
                return null;

            Location target = new()
            {
                Name = form.Name,
                Type = form.Type,
                Address = form.Address,
                ParentLocationId = form.ParentLocationId
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public LocationFormModel MapEntityToDataForm(Location source)
        {
            LocationFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Address = source.Address,
                ParentLocationId = source.ParentLocationId
            };

            return target;
        }

        public List<LocationDataModel> MapCollectionToDataModel(List<Location> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Location? MapUpdateDataFormToEntity(LocationFormModel form, Location current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Type = form.Type;
            current.Address = form.Address;
            current.ParentLocationId = form.ParentLocationId;

            return current;
        }
    }
}
