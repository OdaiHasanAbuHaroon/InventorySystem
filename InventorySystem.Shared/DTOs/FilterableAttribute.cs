using InventorySystem.Shared.Enumerations;

namespace InventorySystem.Shared.DTOs
{
    /// <summary>
    /// Attribute to mark properties as filterable and define their filter criteria.
    /// Can optionally specify a property chain to map nested properties for filtering.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class FilterableAttribute : Attribute
    {
        private readonly string? propertyChain;

        /// <summary>
        /// Gets the property chain used for filtering, if specified.
        /// Represents a path to a nested property (e.g., "Parent.Child.Property").
        /// </summary>
        public string? PropertyChain => propertyChain;

        /// <summary>
        /// Specifies the filter criteria to be applied to the property.
        /// Default is <see cref="FilterCriteria.None"/>.
        /// </summary>
        public FilterCriteria FilterCriteria { get; set; } = FilterCriteria.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableAttribute"/> class
        /// with default settings (no property chain).
        /// </summary>
        public FilterableAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableAttribute"/> class
        /// with a specified property chain for filtering.
        /// </summary>
        /// <param name="propertyChain">
        /// A string representing the path to a nested property (e.g., "Parent.Child.Property").
        /// </param>
        public FilterableAttribute(string propertyChain)
        {
            this.propertyChain = propertyChain;
        }
    }
}
