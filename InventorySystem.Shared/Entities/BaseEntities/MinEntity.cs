using InventorySystem.Shared.Interfaces;
using InventorySystem.Shared.Interfaces.Interceptors;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Shared.Entities.BaseEntities
{
    /// <summary>
    /// Represents a minimal base class for entities that require basic auditing and soft delete functionality.
    /// This class is marked as keyless, meaning it does not have a primary key by design.
    /// </summary>
    [Keyless]
    public abstract class MinEntity : ITimeLessAuditableEntity, ISoftDeletable
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Indicates whether the entity is marked as deleted.
        /// This property is ignored by the Mapperly mapper.
        /// </summary>
        [MapperIgnore]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the source information associated with the entity.
        /// This property is ignored by the Mapperly mapper.
        /// </summary>
        [MapperIgnore]
        public string? Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinEntity"/> class.
        /// Automatically sets the CreatedDate property to the current UTC date and time.
        /// </summary>
        public MinEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
