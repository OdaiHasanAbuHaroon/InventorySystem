using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface ISerialNumberService
    {
        /// <summary>
        /// Retrieves a list of serialNumber based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the serialNumber information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="SerialNumberDataModel"/> objects.</returns>
        Task<GenericResponse<List<SerialNumberDataModel>>> SerialNumberList(SerialNumberFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific serialNumber by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the serialNumber.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="SerialNumberDataModel"/> object.</returns>
        Task<GenericResponse<SerialNumberDataModel>> GetSerialNumber(long id, string lang);

        /// <summary>
        /// Creates a new serialNumber record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the serialNumber's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="SerialNumberDataModel"/> object.</returns>
        Task<GenericResponse<SerialNumberDataModel>> CreateSerialNumber(SerialNumberFormModel form, string lang);

        /// <summary>
        /// Updates an existing serialNumber record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated serialNumber's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="SerialNumberDataModel"/> object.</returns>
        Task<GenericResponse<SerialNumberDataModel>> UpdateSerialNumber(SerialNumberFormModel form, string lang);

        /// <summary>
        /// Deletes a specific serialNumber record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the serialNumber to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteSerialNumber(long id, string lang);

        /// <summary>
        /// Checks if a serialNumber exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the serialNumber's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the serialNumber exists.</returns>
        Task<bool> SerialNumberExists(SerialNumberFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random serialNumber.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="SerialNumberDataModel"/> object.</returns>
        SerialNumberFormModel? GetRandomSerialNumber(string lang);
    }
}
