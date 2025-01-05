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
    /// Service for managing currency-related operations such as creation, updating, retrieval, and deletion.
    /// </summary>
    public class CurrencyService : IIScoped, ICurrencyService
    {
        private readonly ILogger<CurrencyService> _logService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        public CurrencyService(ILogger<CurrencyService> logService, IUnitOfWork unitOfWork, IHelperService helperService, IHttpContextDataProvider connectionStringProvider)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
            _helpService = helperService;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Retrieves a list of currencys based on the filter model.
        /// </summary>
        public GenericResponse<List<CurrencyDataModel>> CurrencyList(CurrencyFilterModel filterModel, string lang)
        {
            try
            {
                var result = CurrencyEnum.GetAll();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CurrencyMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<CurrencyDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Retrieves a currency's details by their ID.
        /// </summary>
        public GenericResponse<CurrencyDataModel> GetCurrency(long id, string lang)
        {
            try
            {
                var current = CurrencyEnum.GetById(id);

                if (current == null)
                    return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CurrencyMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a currency's details by their Name.
        /// </summary>
        public GenericResponse<CurrencyDataModel> GetCurrencyByName(string name, string lang)
        {
            try
            {
                var current = CurrencyEnum.GetByName(name);

                if (current == null)
                    return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CurrencyMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a currency's details by their Code.
        /// </summary>
        public GenericResponse<CurrencyDataModel> GetCurrencyByCode(string code, string lang)
        {
            try
            {
                var current = CurrencyEnum.GetByCode(code);

                if (current == null)
                    return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CurrencyMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CurrencyDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random currency's details.
        /// </summary>
        public CurrencyFormModel? GetRandomCurrency(string lang)
        {
            CurrencyFilterModel filterModel = new();
            List<Currency> allCountries = CurrencyEnum.GetAll(); ;

            if (allCountries != null && allCountries.Count > 0)
            {
                int randomIndex = new Random().Next(allCountries.Count);

                return _mapperService.CurrencyMapper.MapEntityToDataForm(allCountries[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
