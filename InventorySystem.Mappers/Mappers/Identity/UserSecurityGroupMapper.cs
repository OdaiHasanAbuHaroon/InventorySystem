using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class UserSecurityGroupMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<UserSecurityGroupMapper> _logger = loggerFactory.CreateLogger<UserSecurityGroupMapper>();

        public UserSecurityGroupDataModel MapEntityToDataModel(UserSecurityGroup source)
        {
            UserSecurityGroupDataModel target = new();

            if (source.SecurityGroup != null)
            {
                SecurityGroupMapper securityGroupMapper = new(_httpContextDataProvider, _loggerFactory);
                target.SecurityGroup = securityGroupMapper.MapEntityToDataModel(source.SecurityGroup);
            }
            else
            {
                target.SecurityGroup = null;
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

            target.SecurityGroupId = source.SecurityGroupId;
            target.UserId = source.UserId;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<UserSecurityGroupDataModel> MapCollectionToDataModel(List<UserSecurityGroup> collection)
        {
            List<UserSecurityGroupDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
