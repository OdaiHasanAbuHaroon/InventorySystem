using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class BrandMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<BrandMapper> _logger = loggerFactory.CreateLogger<BrandMapper>();

        public BrandDataModel MapEntityToDataModel(Brand source)
        {
            if (source == null)
                return new();

            BrandDataModel target = new();

            // Map navigation properties
            if (source.Manufacturer != null)
            {
                ManufacturerMapper manufacturerMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Manufacturer = manufacturerMapper.MapEntityToDataModel(source.Manufacturer);
            }

            // Map regular properties
            target.Name = source.Name;
            target.Description = source.Description;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Brand? MapDataFormToEntity(BrandFormModel form)
        {
            if (form == null)
                return null;

            Brand target = new()
            {
                Name = form.Name,
                Description = form.Description,
                ManufacturerId = form.ManufacturerId,
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public BrandFormModel MapEntityToDataForm(Brand source)
        {
            BrandFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                ManufacturerId = source.ManufacturerId
            };

            return target;
        }

        public List<BrandDataModel> MapCollectionToDataModel(List<Brand> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Brand? MapUpdateDataFormToEntity(BrandFormModel form, Brand current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Description = form.Description;
            current.ManufacturerId = form.ManufacturerId;

            return current;
        }
    }
}
