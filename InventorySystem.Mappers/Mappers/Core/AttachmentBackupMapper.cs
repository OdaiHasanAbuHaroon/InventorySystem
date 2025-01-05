using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class AttachmentBackupMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<AttachmentBackupMapper> _logger = loggerFactory.CreateLogger<AttachmentBackupMapper>();

        public AttachmentBackupDataModel MapEntityToDataModel(AttachmentBackup source)
        {
            AttachmentBackupDataModel target = new();

            if (source.Attachment != null)
            {
                AttachmentMapper attachmentMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Attachment = attachmentMapper.MapEntityToDataModel(source.Attachment);
            }
            else
            {
                target.Attachment = null;
            }

            target.Name = source.Name;
            target.Extention = source.Extention;
            target.Base64Content = source.Base64Content;
            target.AttachmentId = source.AttachmentId;
            target.Id = source.Id;
            target.FormatDates(_httpContextDataProvider.GetTimeZone());

            return target;
        }

        public List<AttachmentBackupDataModel> MapCollectionToDataModel(List<AttachmentBackup> collection)
        {
            List<AttachmentBackupDataModel> result = [];

            foreach (var item in collection)
            {
                result.Add(MapEntityToDataModel(item));
            }

            return result;
        }
    }
}
