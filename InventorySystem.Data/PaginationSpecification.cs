namespace InventorySystem.Data
{
    public class PaginationSpecification<T> : SpecificationBase<T>
     where T : class
    {
        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }
}
