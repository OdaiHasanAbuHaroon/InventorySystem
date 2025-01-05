using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface IBrandService
    {
        /// <summary>
        /// Retrieves a list of brand based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the brand information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="BrandDataModel"/> objects.</returns>
        Task<GenericResponse<List<BrandDataModel>>> BrandList(BrandFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific brand by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the brand.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="BrandDataModel"/> object.</returns>
        Task<GenericResponse<BrandDataModel>> GetBrand(long id, string lang);

        /// <summary>
        /// Creates a new brand record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the brand's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="BrandDataModel"/> object.</returns>
        Task<GenericResponse<BrandDataModel>> CreateBrand(BrandFormModel form, string lang);

        /// <summary>
        /// Updates an existing brand record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated brand's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="BrandDataModel"/> object.</returns>
        Task<GenericResponse<BrandDataModel>> UpdateBrand(BrandFormModel form, string lang);

        /// <summary>
        /// Deletes a specific brand record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the brand to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteBrand(long id, string lang);

        /// <summary>
        /// Checks if a brand exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the brand's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the brand exists.</returns>
        Task<bool> BrandExists(BrandFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random brand.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="BrandDataModel"/> object.</returns>
        BrandFormModel? GetRandomBrand(string lang);
    }
}
