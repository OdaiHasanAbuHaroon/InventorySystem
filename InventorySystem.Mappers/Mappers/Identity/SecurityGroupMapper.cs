using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class SecurityGroupMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<SecurityGroupMapper> _logger = loggerFactory.CreateLogger<SecurityGroupMapper>();

        public SecurityGroupDataModel MapEntityToDataModel(SecurityGroup source)
        {
            SecurityGroupDataModel target = new()
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

        public SecurityGroup MapDataFormToEntity(SecurityGroupFormModel form)
        {
            SecurityGroup target = new()
            {
                Name = form.Name,
                Description = form.Description,
            };

            return target;
        }

        public List<SecurityGroupDataModel> MapCollectionToDataModel(List<SecurityGroup> collection)
        {
            List<SecurityGroupDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }

    }
}
