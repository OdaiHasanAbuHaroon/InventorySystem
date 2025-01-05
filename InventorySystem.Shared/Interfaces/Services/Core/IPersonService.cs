using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves a list of people based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the person information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="PersonDataModel"/> objects.</returns>
        Task<GenericResponse<List<PersonDataModel>>> PersonList(PersonFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific person by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="PersonDataModel"/> object.</returns>
        Task<GenericResponse<PersonDataModel>> GetPerson(long id, string lang);

        /// <summary>
        /// Creates a new person record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the person's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="PersonDataModel"/> object.</returns>
        Task<GenericResponse<PersonDataModel>> CreatePerson(PersonFormModel form, string lang);

        /// <summary>
        /// Updates an existing person record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated person's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="PersonDataModel"/> object.</returns>
        Task<GenericResponse<PersonDataModel>> UpdatePerson(PersonFormModel form, string lang);

        /// <summary>
        /// Deletes a specific person record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeletePerson(long id, string lang);

        /// <summary>
        /// Checks if a person exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the person's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the person exists.</returns>
        Task<bool> PersonExists(PersonFormModel form, bool checkDifferentId = true);
    }
}
