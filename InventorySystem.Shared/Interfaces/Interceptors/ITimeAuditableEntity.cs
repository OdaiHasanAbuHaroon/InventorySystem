namespace InventorySystem.Shared.Interfaces.Interceptors
{
    /// <summary>
    /// Represents an interface for entities that require time-based auditing.
    /// Includes properties for tracking creation, modification, and source details.
    /// </summary>
    public interface ITimeAuditableEntity
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the entity.
        /// </summary>
        string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the source information related to the entity.
        /// Useful for tracking the origin of data.
        /// </summary>
        string? Source { get; set; }
    }
}
