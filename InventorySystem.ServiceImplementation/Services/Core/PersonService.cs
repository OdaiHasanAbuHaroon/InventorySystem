using InventorySystem.Mappers;
using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    /// <summary>
    /// Service for managing person-related operations such as creation, updating, retrieval, and deletion.
    /// </summary>
    public class PersonService : IIScoped, IPersonService
    {
        private readonly ILogger<PersonService> _logService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        public PersonService(ILogger<PersonService> logService, IUnitOfWork unitOfWork, IHelperService helperService, IHttpContextDataProvider connectionStringProvider)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
            _helpService = helperService;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a person exists based on the provided form.
        /// </summary>
        public async Task<bool> PersonExists(PersonFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.PersonRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a list of persons based on the filter model.
        /// </summary>
        public async Task<GenericResponse<List<PersonDataModel>>> PersonList(PersonFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.PersonRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.PersonMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<PersonDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        public async Task<GenericResponse<PersonDataModel>> CreatePerson(PersonFormModel form, string lang)
        {
            try
            {
                if (await PersonExists(form))
                    return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.PersonMapper.MapDataFormToEntity(form);
                await _unitOfWork.PersonRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.PersonMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing person's details.
        /// </summary>
        public async Task<GenericResponse<PersonDataModel>> UpdatePerson(PersonFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.PersonRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.record_notfound, lang);

                if (await PersonExists(form, true))
                    return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.PersonMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.PersonRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.PersonMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        public async Task<GenericResponse<bool>> DeletePerson(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.PersonRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.PersonRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a person's details by their ID.
        /// </summary>
        public async Task<GenericResponse<PersonDataModel>> GetPerson(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.PersonRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.PersonMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<PersonDataModel>(MessageKeys.system_error, lang, ex);
            }
        }
    }
}
