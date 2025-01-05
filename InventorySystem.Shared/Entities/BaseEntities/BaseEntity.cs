using InventorySystem.Shared.Interfaces;
using InventorySystem.Shared.Interfaces.Interceptors;
using Riok.Mapperly.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Shared.Entities.BaseEntities
{
    /// <summary>
    /// Represents the base class for all entities in the InventoryDemo system.
    /// Provides common properties such as Id, audit fields, and soft delete functionality.
    /// This class can be inherited by other entities to standardize their structure.
    /// </summary>
    public abstract class BaseEntity : ISoftDeletable, ITimeAuditableEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates whether the entity is marked as deleted.
        /// This property is ignored by the Mapperly mapper.
        /// </summary>
        [MapperIgnore]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the entity.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Indicates whether the entity is currently active.
        /// This property is ignored by the Mapperly mapper.
        /// </summary>
        [MapperIgnore]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the source information associated with the entity.
        /// This property is ignored by the Mapperly mapper.
        /// </summary>
        [MapperIgnore]
        public string? Source { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// Automatically sets the CreatedDate property to the current UTC date and time.
        /// </summary>
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
