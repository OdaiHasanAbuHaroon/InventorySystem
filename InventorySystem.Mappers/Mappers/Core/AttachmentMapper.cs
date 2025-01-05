using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class AttachmentMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<AttachmentMapper> _logger = loggerFactory.CreateLogger<AttachmentMapper>();

        public AttachmentDataModel MapEntityToDataModel(Attachment source)
        {
            AttachmentDataModel target = new()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Extention = source.Extention,
                Id = source.Id,
                ModifiedBy = source.ModifiedBy,
                ModifiedDate = source.ModifiedDate,
                Name = source.Name,
                FileContent = null,
                Path = $"/AttachmentStore?id={source.Id}"
            };

            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<AttachmentDataModel> MapCollectionToDataModel(List<Attachment> collection)
        {
            List<AttachmentDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}