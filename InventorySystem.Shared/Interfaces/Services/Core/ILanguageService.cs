using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface ILanguageService
    {
        /// <summary>
        /// Retrieves a list of languages based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the languages.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="LanguageDataModel"/> objects.</returns>
        Task<GenericResponse<List<LanguageDataModel>>> LanguageList(LanguageFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific language by its ID and language.
        /// </summary>
        /// <param name="Id">The unique identifier of the language.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="LanguageDataModel"/> object.</returns>
        Task<GenericResponse<LanguageDataModel>> GetLanguage(long Id, string lang);
    }

}
