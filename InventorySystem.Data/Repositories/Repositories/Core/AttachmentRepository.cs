using InventorySystem.Shared.Entities.Core;
using InventorySystem.Data.DataContext;
using InventorySystem.Data.Repositories.IRepositories.Core;

namespace InventorySystem.Data.Repositories.Repositories.Core
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(DatabaseContext context) : base(context) { }
    }
}