using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.BaseDTOs
{
    /// <summary>
    /// Represents a base model for filtering, sorting, and pagination of data.
    /// Supports compatibility with both Newtonsoft.Json and System.Text.Json for serialization.
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Gets or sets the current page number for pagination.
        /// Default is 1.
        /// </summary>
        [JsonPropertyName("CurrentPage")]
        [JsonProperty("CurrentPage")]
        public int? CurrentPage { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of records per page for pagination.
        /// Default is 20.
        /// </summary>
        [JsonPropertyName("PageSize")]
        [JsonProperty("PageSize")]
        public int? PageSize { get; set; } = 20;

        /// <summary>
        /// Gets or sets the total number of pages available.
        /// Typically calculated based on TotalRecords and PageSize.
        /// </summary>
        [JsonPropertyName("TotalPages")]
        [JsonProperty("TotalPages")]
        public int? TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total number of records available.
        /// Used to calculate pagination details.
        /// </summary>
        [JsonPropertyName("TotalRecords")]
        [JsonProperty("TotalRecords")]
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// True for ascending and false for descending. Default is true.
        /// </summary>
        [JsonPropertyName("SortDirection")]
        [JsonProperty("SortDirection")]
        public bool? SortDirection { get; set; } = true;

        /// <summary>
        /// Gets or sets the parameter to sort the data by.
        /// </summary>
        [JsonPropertyName("SortParameter")]
        [JsonProperty("SortParameter")]
        public string? SortParameter { get; set; }

        /// <summary>
        /// Gets or sets the ordering information in a string format.
        /// Used for custom ordering scenarios.
        /// </summary>
        [JsonPropertyName("Ordering")]
        [JsonProperty("Ordering")]
        public string? Ordering { get; set; }

        /// <summary>
        /// Indicates whether the records being filtered are active.
        /// This property is marked as filterable and is true by default.
        /// </summary>
        [JsonPropertyName("IsActive")]
        [JsonProperty("IsActive")]
        [Filterable]
        public bool IsActive { get; private set; } = true;

        /// <summary>
        /// Gets or sets the unique identifier to filter by.
        /// This property is marked as filterable.
        /// </summary>
        [JsonPropertyName("Id")]
        [JsonProperty("Id")]
        [Filterable]
        public long? Id { get; set; }
    }
}
