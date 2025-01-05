using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class ModuleMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<ModuleMapper> _logger = loggerFactory.CreateLogger<ModuleMapper>();

        public ModuleDataModel MapEntityToDataModel(Module source)
        {
            ModuleDataModel target = new()
            {
                Name = source.Name,
                Description = source.Description,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<ModuleDataModel> MapCollectionToDataModel(List<Module> collection)
        {
            List<ModuleDataModel> result = [];

            foreach (var module in collection)
            {
                result.Add(MapEntityToDataModel(module));
            }

            return result;
        }
    }
}
