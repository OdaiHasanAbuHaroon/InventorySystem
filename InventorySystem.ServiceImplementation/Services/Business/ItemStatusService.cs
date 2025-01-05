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
    /// Provides services for managing item statuses, including CRUD operations and validation.
    /// </summary>
    public class ItemStatusService : IIScoped, IItemStatusService
    {
        private readonly ILogger<ItemStatusService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStatusService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public ItemStatusService(ILogger<ItemStatusService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if an item status with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The item status form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the item status exists; otherwise, false.</returns>
        public async Task<bool> ItemStatusExists(ItemStatusFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.ItemStatusRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of item statuses based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for item status data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of item status data models.</returns>
        public async Task<GenericResponse<List<ItemStatusDataModel>>> ItemStatusList(ItemStatusFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.ItemStatusRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemStatusMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ItemStatusDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new item status record in the database.
        /// </summary>
        /// <param name="form">The item status form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created item status data model.</returns>
        public async Task<GenericResponse<ItemStatusDataModel>> CreateItemStatus(ItemStatusFormModel form, string lang)
        {
            try
            {
                if (await ItemStatusExists(form))
                    return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.ItemStatusMapper.MapDataFormToEntity(form);
                await _unitOfWork.ItemStatusRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemStatusMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing item status record in the database.
        /// </summary>
        /// <param name="form">The item status form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated item status data model.</returns>
        public async Task<GenericResponse<ItemStatusDataModel>> UpdateItemStatus(ItemStatusFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemStatusRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.record_notfound, lang);

                if (await ItemStatusExists(form, true))
                    return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.ItemStatusMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.ItemStatusRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemStatusMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing item status record from the database.
        /// </summary>
        /// <param name="id">The ID of the item status to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteItemStatus(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemStatusRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.ItemStatusRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves an item status by its ID.
        /// </summary>
        /// <param name="id">The ID of the item status to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the item status data model.</returns>
        public async Task<GenericResponse<ItemStatusDataModel>> GetItemStatus(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemStatusRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemStatusMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random itemStatus's details.
        /// </summary>
        public ItemStatusFormModel? GetRandomItemStatus(string lang)
        {
            ItemStatusFilterModel filterModel = new();
            List<ItemStatus> allItemStatuss = _unitOfWork.ItemStatusRepository.GetAll().Result.ToList();

            if (allItemStatuss != null && allItemStatuss.Count > 0)
            {
                int randomIndex = new Random().Next(allItemStatuss.Count);

                return _mapperService.ItemStatusMapper.MapEntityToDataForm(allItemStatuss[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
