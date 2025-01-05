using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Retrieves a list of people based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the currency information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="CurrencyDataModel"/> objects.</returns>
        GenericResponse<List<CurrencyDataModel>> CurrencyList(CurrencyFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific currency by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the currency.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CurrencyDataModel"/> object.</returns>
        GenericResponse<CurrencyDataModel> GetCurrency(long id, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific currency by their name and language.
        /// </summary>
        /// <param name="name">The name of the currency.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CurrencyDataModel"/> object.</returns>
        GenericResponse<CurrencyDataModel> GetCurrencyByName(string name, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific currency by their name and language.
        /// </summary>
        /// <param name="code">The code of the currency.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CurrencyDataModel"/> object.</returns>
        GenericResponse<CurrencyDataModel> GetCurrencyByCode(string code, string lang);
    }
}
