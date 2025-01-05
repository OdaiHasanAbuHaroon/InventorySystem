using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves a list of category based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the category information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="CategoryDataModel"/> objects.</returns>
        Task<GenericResponse<List<CategoryDataModel>>> CategoryList(CategoryFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific category by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CategoryDataModel"/> object.</returns>
        Task<GenericResponse<CategoryDataModel>> GetCategory(long id, string lang);

        /// <summary>
        /// Creates a new category record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the category's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="CategoryDataModel"/> object.</returns>
        Task<GenericResponse<CategoryDataModel>> CreateCategory(CategoryFormModel form, string lang);

        /// <summary>
        /// Updates an existing category record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated category's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="CategoryDataModel"/> object.</returns>
        Task<GenericResponse<CategoryDataModel>> UpdateCategory(CategoryFormModel form, string lang);

        /// <summary>
        /// Deletes a specific category record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteCategory(long id, string lang);

        /// <summary>
        /// Checks if a category exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the category's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the category exists.</returns>
        Task<bool> CategoryExists(CategoryFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random category.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CategoryDataModel"/> object.</returns>
        CategoryFormModel? GetRandomCategory(string lang);
    }
}
