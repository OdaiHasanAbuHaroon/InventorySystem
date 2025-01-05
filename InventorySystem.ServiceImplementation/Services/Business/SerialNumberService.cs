using InventorySystem.Mappers;
using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Business;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.ServiceImplementation;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySystem.RepositoryImplementation.Repositorys.Business
{
    /// <summary>
    /// Provides services for managing serial numbers, including CRUD operations and validation.
    /// </summary>
    public class SerialNumberService : IIScoped, ISerialNumberService
    {
        private readonly ILogger<SerialNumberService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialNumberService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public SerialNumberService(ILogger<SerialNumberService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a serial number with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The serial number form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the serial number exists; otherwise, false.</returns>
        public async Task<bool> SerialNumberExists(SerialNumberFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.SerialNumberRepository.AnyAsync(x => x.Serial == form.Serial && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of serial numbers based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for serial number data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of serial number data models.</returns>
        public async Task<GenericResponse<List<SerialNumberDataModel>>> SerialNumberList(SerialNumberFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.SerialNumberRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SerialNumberMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<SerialNumberDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new serial number record in the database.
        /// </summary>
        /// <param name="form">The serial number form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created serial number data model.</returns>
        public async Task<GenericResponse<SerialNumberDataModel>> CreateSerialNumber(SerialNumberFormModel form, string lang)
        {
            try
            {
                if (await SerialNumberExists(form))
                    return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.SerialNumberMapper.MapDataFormToEntity(form);
                await _unitOfWork.SerialNumberRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SerialNumberMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing serial number record in the database.
        /// </summary>
        /// <param name="form">The serial number form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated serial number data model.</returns>
        public async Task<GenericResponse<SerialNumberDataModel>> UpdateSerialNumber(SerialNumberFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.SerialNumberRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.record_notfound, lang);

                if (await SerialNumberExists(form, true))
                    return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.SerialNumberMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.SerialNumberRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SerialNumberMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing serial number record from the database.
        /// </summary>
        /// <param name="id">The ID of the serial number to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteSerialNumber(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.SerialNumberRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.SerialNumberRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a serial number by its ID.
        /// </summary>
        /// <param name="id">The ID of the serial number to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the serial number data model.</returns>
        public async Task<GenericResponse<SerialNumberDataModel>> GetSerialNumber(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.SerialNumberRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SerialNumberMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random serialNumber's details.
        /// </summary>
        public SerialNumberFormModel? GetRandomSerialNumber(string lang)
        {
            SerialNumberFilterModel filterModel = new();
            List<SerialNumber> allSerialNumbers = _unitOfWork.SerialNumberRepository.GetAll().Result.ToList();

            if (allSerialNumbers != null && allSerialNumbers.Count > 0)
            {
                int randomIndex = new Random().Next(allSerialNumbers.Count);

                return _mapperService.SerialNumberMapper.MapEntityToDataForm(allSerialNumbers[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
