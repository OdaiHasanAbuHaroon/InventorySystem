using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface IItemService
    {
        /// <summary>
        /// Retrieves a list of item based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the item information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="ItemDataModel"/> objects.</returns>
        Task<GenericResponse<List<ItemDataModel>>> ItemList(ItemFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific item by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the item.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ItemDataModel"/> object.</returns>
        Task<GenericResponse<ItemDataModel>> GetItem(long id, string lang);

        /// <summary>
        /// Creates a new item record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the item's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="ItemDataModel"/> object.</returns>
        Task<GenericResponse<ItemDataModel>> CreateItem(ItemFormModel form, string lang);

        /// <summary>
        /// Updates an existing item record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated item's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="ItemDataModel"/> object.</returns>
        Task<GenericResponse<ItemDataModel>> UpdateItem(ItemFormModel form, string lang);

        /// <summary>
        /// Deletes a specific item record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteItem(long id, string lang);

        /// <summary>
        /// Checks if a item exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the item's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the item exists.</returns>
        Task<bool> ItemExists(ItemFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random item.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="ItemDataModel"/> object.</returns>
        ItemFormModel? GetRandomItem(string lang);
    }
}
