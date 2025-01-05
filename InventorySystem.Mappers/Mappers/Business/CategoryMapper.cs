using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class CategoryMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<CategoryMapper> _logger = loggerFactory.CreateLogger<CategoryMapper>();

        public CategoryDataModel MapEntityToDataModel(Category source)
        {
            if (source == null)
                return new();

            CategoryDataModel target = new();

            // Map regular properties
            target.Name = source.Name;
            target.Description = source.Description;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Category? MapDataFormToEntity(CategoryFormModel form)
        {
            if (form == null)
                return null;

            Category target = new()
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

        public CategoryFormModel MapEntityToDataForm(Category source)
        {
            CategoryFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description
            };

            return target;
        }

        public List<CategoryDataModel> MapCollectionToDataModel(List<Category> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Category? MapUpdateDataFormToEntity(CategoryFormModel form, Category current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Description = form.Description;

            return current;
        }
    }
}
