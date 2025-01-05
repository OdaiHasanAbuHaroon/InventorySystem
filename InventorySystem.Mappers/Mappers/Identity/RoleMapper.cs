using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class RoleMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<RoleMapper> _logger = loggerFactory.CreateLogger<RoleMapper>();

        public RoleDataModel MapEntityToDataModel(Role source)
        {
            RoleDataModel target = new()
            {
                RoleName = source.RoleName,
                RoleDescription = source.RoleDescription,
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                CreatedBy = source.CreatedBy,
                ModifiedDate = source.ModifiedDate,
                ModifiedBy = source.ModifiedBy
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<RoleDataModel> MapCollectionToDataModel(List<Role> collection)
        {
            List<RoleDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
