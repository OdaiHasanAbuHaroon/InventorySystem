using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface IManufacturerService
    {
        /// <summary>
        /// Retrieves a list of manufacturer based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the manufacturer information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="ManufacturerDataModel"/> objects.</returns>
        Task<GenericResponse<List<ManufacturerDataModel>>> ManufacturerList(ManufacturerFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific manufacturer by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ManufacturerDataModel"/> object.</returns>
        Task<GenericResponse<ManufacturerDataModel>> GetManufacturer(long id, string lang);

        /// <summary>
        /// Creates a new manufacturer record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the manufacturer's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="ManufacturerDataModel"/> object.</returns>
        Task<GenericResponse<ManufacturerDataModel>> CreateManufacturer(ManufacturerFormModel form, string lang);

        /// <summary>
        /// Updates an existing manufacturer record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated manufacturer's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="ManufacturerDataModel"/> object.</returns>
        Task<GenericResponse<ManufacturerDataModel>> UpdateManufacturer(ManufacturerFormModel form, string lang);

        /// <summary>
        /// Deletes a specific manufacturer record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteManufacturer(long id, string lang);

        /// <summary>
        /// Checks if a manufacturer exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the manufacturer's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the manufacturer exists.</returns>
        Task<bool> ManufacturerExists(ManufacturerFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random manufacturer.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ManufacturerDataModel"/> object.</returns>
        ManufacturerFormModel? GetRandomManufacturer(string lang);
    }
}
