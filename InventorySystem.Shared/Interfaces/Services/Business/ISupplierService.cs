using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Business
{
    public interface ISupplierService
    {
        /// <summary>
        /// Retrieves a list of supplier based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the supplier information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="SupplierDataModel"/> objects.</returns>
        Task<GenericResponse<List<SupplierDataModel>>> SupplierList(SupplierFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific supplier by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the supplier.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="SupplierDataModel"/> object.</returns>
        Task<GenericResponse<SupplierDataModel>> GetSupplier(long id, string lang);

        /// <summary>
        /// Creates a new supplier record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the supplier's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="SupplierDataModel"/> object.</returns>
        Task<GenericResponse<SupplierDataModel>> CreateSupplier(SupplierFormModel form, string lang);

        /// <summary>
        /// Updates an existing supplier record using the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the updated supplier's data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the updated <see cref="SupplierDataModel"/> object.</returns>
        Task<GenericResponse<SupplierDataModel>> UpdateSupplier(SupplierFormModel form, string lang);

        /// <summary>
        /// Deletes a specific supplier record by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the supplier to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteSupplier(long id, string lang);

        /// <summary>
        /// Checks if a supplier exists in the system based on the specified form data.
        /// </summary>
        /// <param name="form">The form model containing the supplier's data to check.</param>
        /// <param name="checkDifferentId">Indicates whether to check for existence excluding a different ID.</param>
        /// <returns>A boolean value indicating whether the supplier exists.</returns>
        Task<bool> SupplierExists(SupplierFormModel form, bool checkDifferentId = true);

        /// <summary>
        /// Retrieves detailed information for a random supplier.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="SupplierDataModel"/> object.</returns>
        SupplierFormModel? GetRandomSupplier(string lang);
    }
}
