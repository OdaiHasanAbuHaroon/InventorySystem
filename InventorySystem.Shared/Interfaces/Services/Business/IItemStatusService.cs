using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface IItemStatusService
    {
        /// <summary>
        /// Retrieves a list of itemStatus based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the itemStatus information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="ItemStatusDataModel"/> objects.</returns>
        Task<GenericResponse<List<ItemStatusDataModel>>> ItemStatusList(ItemStatusFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific itemStatus by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the itemStatus.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ItemStatusDataModel"/> object.</returns>
        Task<GenericResponse<ItemStatusDataModel>> GetItemStatus(long id, string lang);

        /// <summary>
        /// Creates a new itemStatus record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the itemStatus's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="ItemStatusDataModel"/> object.</returns>
        Task<GenericResponse<ItemStatusDataModel>> CreateItemStatus(ItemStatusFormModel form, string lang);

        /// <summary>
        /// Updates an existing itemStatus record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated itemStatus's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="ItemStatusDataModel"/> object.</returns>
        Task<GenericResponse<ItemStatusDataModel>> UpdateItemStatus(ItemStatusFormModel form, string lang);

        /// <summary>
        /// Deletes a specific itemStatus record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the itemStatus to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteItemStatus(long id, string lang);

        /// <summary>
        /// Checks if a itemStatus exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the itemStatus's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the itemStatus exists.</returns>
        Task<bool> ItemStatusExists(ItemStatusFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random itemStatus.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ItemStatusDataModel"/> object.</returns>
        ItemStatusFormModel? GetRandomItemStatus(string lang);
    }
}
