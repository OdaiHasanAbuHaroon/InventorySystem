using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface ILocationService
    {
        /// <summary>
        /// Retrieves a list of location based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the location information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="LocationDataModel"/> objects.</returns>
        Task<GenericResponse<List<LocationDataModel>>> LocationList(LocationFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific location by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the location.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="LocationDataModel"/> object.</returns>
        Task<GenericResponse<LocationDataModel>> GetLocation(long id, string lang);

        /// <summary>
        /// Creates a new location record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the location's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="LocationDataModel"/> object.</returns>
        Task<GenericResponse<LocationDataModel>> CreateLocation(LocationFormModel form, string lang);

        /// <summary>
        /// Updates an existing location record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated location's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="LocationDataModel"/> object.</returns>
        Task<GenericResponse<LocationDataModel>> UpdateLocation(LocationFormModel form, string lang);

        /// <summary>
        /// Deletes a specific location record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the location to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteLocation(long id, string lang);

        /// <summary>
        /// Checks if a location exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the location's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the location exists.</returns>
        Task<bool> LocationExists(LocationFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random location.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="LocationDataModel"/> object.</returns>
        LocationFormModel? GetRandomLocation(string lang);
    }
}
