using InventorySystem.Mappers;
using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Entities.Enumerations.SeedEnumeration;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    /// <summary>
    /// Service for managing country-related operations such as creation, updating, retrieval, and deletion.
    /// </summary>
    public class CountryService : IIScoped, ICountryService
    {
        private readonly ILogger<CountryService> _logService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService"/> class.
        /// </summary>
        public CountryService(ILogger<CountryService> logService, IUnitOfWork unitOfWork, IHelperService helperService, IHttpContextDataProvider connectionStringProvider)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
            _helpService = helperService;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Retrieves a list of countrys based on the filter model.
        /// </summary>
        public GenericResponse<List<CountryDataModel>> CountryList(CountryFilterModel filterModel, string lang)
        {
            try
            {
                var result = CountryEnum.GetAll();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CountryMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<CountryDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Retrieves a country's details by their ID.
        /// </summary>
        public GenericResponse<CountryDataModel> GetCountry(long id, string lang)
        {
            try
            {
                var current = CountryEnum.GetById(id);

                if (current == null)
                    return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CountryMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a country's details by their Name.
        /// </summary>
        public GenericResponse<CountryDataModel> GetCountryByName(string name, string lang)
        {
            try
            {
                var current = CountryEnum.GetByName(name);

                if (current == null)
                    return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CountryMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a country's details by their Code.
        /// </summary>
        public GenericResponse<CountryDataModel> GetCountryByCode(string code, string lang)
        {
            try
            {
                var current = CountryEnum.GetByCode(code);

                if (current == null)
                    return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CountryMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CountryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random country's details.
        /// </summary>
        public CountryFormModel? GetRandomCountry(string lang)
        {
            CountryFilterModel filterModel = new();
            List<Country> allCountries = CountryEnum.GetAll(); ;

            if (allCountries != null && allCountries.Count > 0)
            {
                int randomIndex = new Random().Next(allCountries.Count);

                return _mapperService.CountryMapper.MapEntityToDataForm(allCountries[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
