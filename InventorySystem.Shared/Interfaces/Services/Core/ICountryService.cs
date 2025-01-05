using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface ICountryService
    {
        /// <summary>
        /// Retrieves a list of people based on the specified filter and language.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for filtering the country information.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="CountryDataModel"/> objects.</returns>
        GenericResponse<List<CountryDataModel>> CountryList(CountryFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific country by their ID and language.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CountryDataModel"/> object.</returns>
        GenericResponse<CountryDataModel> GetCountry(long id, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific country by their name and language.
        /// </summary>
        /// <param name="name">The name of the country.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CountryDataModel"/> object.</returns>
        GenericResponse<CountryDataModel> GetCountryByName(string name, string lang);

        /// <summary>
        /// Retrieves detailed information for a specific country by their name and language.
        /// </summary>
        /// <param name="code">The code of the country.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CountryDataModel"/> object.</returns>
        GenericResponse<CountryDataModel> GetCountryByCode(string code, string lang);

        /// <summary>
        /// Retrieves detailed information for a random country.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the <see cref="CountryDataModel"/> object.</returns>
        CountryFormModel? GetRandomCountry(string lang);
    }
}
