using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class UserRoleMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<UserRoleMapper> _logger = loggerFactory.CreateLogger<UserRoleMapper>();

        public UserRoleDataModel MapEntityToDataModel(UserRole source)
        {
            UserRoleDataModel target = new();

            if (source.Role != null)
            {
                RoleMapper roleMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Role = roleMapper.MapEntityToDataModel(source.Role);
            }
            else
            {
                target.Role = null;
            }

            if (source.User != null)
            {
                UserMapper userMapper = new(_httpContextDataProvider, _loggerFactory);
                target.User = userMapper.MapEntityToDataModel(source.User);
            }
            else
            {
                target.User = null;
            }

            target.RoleId = source.RoleId;
            target.UserId = source.UserId;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<UserRoleDataModel> MapCollectionToDataModel(List<UserRole> collection)
        {
            List<UserRoleDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
