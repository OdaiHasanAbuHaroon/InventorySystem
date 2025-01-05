using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    /// <summary>
    /// Interface for managing time zone information services.
    /// </summary>
    public interface ITimeZoneInformationService
    {
        /// <summary>
        /// Retrieves a list of time zone information based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the time zone information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="TimeZoneInformationDataModel"/> objects.</returns>
        Task<GenericResponse<List<TimeZoneInformationDataModel>>> TimeZoneInformationList(TimeZoneInformationFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed time zone information for a specific time zone by its ID and language.
        /// </summary>
        /// <param name="Id">The unique identifier of the time zone information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="TimeZoneInformationDataModel"/> object.</returns>
        Task<GenericResponse<TimeZoneInformationDataModel>> GetTimeZoneInformation(long Id, string lang);
    }
}
