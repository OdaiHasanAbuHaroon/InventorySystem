using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class UserModuleMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<UserModuleMapper> _logger = loggerFactory.CreateLogger<UserModuleMapper>();

        public UserModuleDataModel MapEntityToDataModel(UserModule source)
        {
            var target = new UserModuleDataModel();

            if (source.User != null)
            {
                UserMapper userMapper = new(_httpContextDataProvider, _loggerFactory);
                target.User = userMapper.MapEntityToDataModel(source.User);
            }
            else
            {
                target.User = null;
            }

            if (source.Module != null)
            {
                ModuleMapper moduleMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Module = moduleMapper.MapEntityToDataModel(source.Module);
            }
            else
            {
                target.Module = null;
            }

            target.UserId = source.UserId;
            target.ModuleId = source.ModuleId;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<UserModuleDataModel> MapCollectionToDataModel(List<UserModule> collection)
        {
            List<UserModuleDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }

    }
}
