using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class ItemMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<ItemMapper> _logger = loggerFactory.CreateLogger<ItemMapper>();

        public ItemDataModel MapEntityToDataModel(Item source)
        {
            if (source == null)
                return new();

            ItemDataModel target = new();

            // Map navigation properties
            if (source.Brand != null)
            {
                BrandMapper brandMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Brand = brandMapper.MapEntityToDataModel(source.Brand);
            }

            if (source.Category != null)
            {
                CategoryMapper categoryMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Category = categoryMapper.MapEntityToDataModel(source.Category);
            }

            if (source.ItemStatus != null)
            {
                ItemStatusMapper itemStatusMapper = new(_httpContextDataProvider, _loggerFactory);
                target.ItemStatus = itemStatusMapper.MapEntityToDataModel(source.ItemStatus);
            }

            if (source.Location != null)
            {
                LocationMapper locationMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Location = locationMapper.MapEntityToDataModel(source.Location);
            }

            // Map regular properties
            target.Name = source.Name;
            target.Description = source.Description;
            target.UnitOfMeasurement = source.UnitOfMeasurement;
            target.Serialized = source.Serialized;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Item? MapDataFormToEntity(ItemFormModel form)
        {
            if (form == null)
                return null;

            Item target = new()
            {
                Name = form.Name,
                Description = form.Description,
                UnitOfMeasurement = form.UnitOfMeasurement,
                Serialized = form.Serialized,
                BrandId = form.BrandId,
                CategoryId = form.CategoryId,
                ItemStatusId = form.ItemStatusId,
                LocationId = form.LocationId,
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public ItemFormModel MapEntityToDataForm(Item source)
        {
            ItemFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                UnitOfMeasurement = source.UnitOfMeasurement,
                Serialized = source.Serialized,
                BrandId = source.BrandId,
                CategoryId = source.CategoryId,
                ItemStatusId = source.ItemStatusId,
                LocationId = source.LocationId,
            };

            return target;
        }

        public List<ItemDataModel> MapCollectionToDataModel(List<Item> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Item? MapUpdateDataFormToEntity(ItemFormModel form, Item current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Description = form.Description;
            current.UnitOfMeasurement = form.UnitOfMeasurement;
            current.Serialized = form.Serialized;
            current.BrandId = form.BrandId;
            current.CategoryId = form.CategoryId;
            current.ItemStatusId = form.ItemStatusId;
            current.LocationId = form.LocationId;

            return current;
        }
    }
}
