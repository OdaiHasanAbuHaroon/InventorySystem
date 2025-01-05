namespace InventorySystem.Shared.Interfaces.Interceptors
{
    /// <summary>
    /// Represents an interface for entities requiring minimal auditing.
    /// Includes properties for creation details and source information without modification tracking.
    /// </summary>
    public interface ITimeLessAuditableEntity
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
        /// Gets or sets the source information related to the entity.
        /// Useful for tracking the origin of data.
        /// </summary>
        string? Source { get; set; }
    }
}
