using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class TwofactorMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<TwofactorMapper> _logger = loggerFactory.CreateLogger<TwofactorMapper>();

        public TwofactorDataModel MapEntityToDataModel(Twofactor source)
        {
            TwofactorDataModel target = new();

            if (source.User != null)
            {
                UserMapper userMapper = new(_httpContextDataProvider, _loggerFactory);
                target.User = userMapper.MapEntityToDataModel(source.User);
            }
            else
            {
                target.User = null;
            }

            target.UserId = source.UserId;
            target.Code = source.Code;
            target.ExpirationDate = source.ExpirationDate;
            target.IsUsed = source.IsUsed;
            target.IsSent = source.IsSent;
            target.Stamp = source.Stamp;
            target.RequestType = source.RequestType;
            target.Id = source.Id;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.ModifiedBy = source.ModifiedBy;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<TwofactorDataModel> MapCollectionToDataModel(List<Twofactor> collection)
        {
            List<TwofactorDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }

    }
}
