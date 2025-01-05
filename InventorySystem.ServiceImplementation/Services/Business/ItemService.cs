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
    /// Provides services for managing items, including CRUD operations and validation.
    /// </summary>
    public class ItemService : IIScoped, IItemService
    {
        private readonly ILogger<ItemService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public ItemService(ILogger<ItemService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if an item with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The item form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        public async Task<bool> ItemExists(ItemFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.ItemRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of items based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for item data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of item data models.</returns>
        public async Task<GenericResponse<List<ItemDataModel>>> ItemList(ItemFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.ItemRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ItemDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new item record in the database.
        /// </summary>
        /// <param name="form">The item form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created item data model.</returns>
        public async Task<GenericResponse<ItemDataModel>> CreateItem(ItemFormModel form, string lang)
        {
            try
            {
                if (await ItemExists(form))
                    return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.ItemMapper.MapDataFormToEntity(form);
                await _unitOfWork.ItemRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing item record in the database.
        /// </summary>
        /// <param name="form">The item form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated item data model.</returns>
        public async Task<GenericResponse<ItemDataModel>> UpdateItem(ItemFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.record_notfound, lang);

                if (await ItemExists(form, true))
                    return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.ItemMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.ItemRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing item record from the database.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteItem(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.ItemRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the item data model.</returns>
        public async Task<GenericResponse<ItemDataModel>> GetItem(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ItemRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ItemMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random item's details.
        /// </summary>
        public ItemFormModel? GetRandomItem(string lang)
        {
            ItemFilterModel filterModel = new();
            List<Item> allItems = _unitOfWork.ItemRepository.GetAll().Result.ToList();

            if (allItems != null && allItems.Count > 0)
            {
                int randomIndex = new Random().Next(allItems.Count);

                return _mapperService.ItemMapper.MapEntityToDataForm(allItems[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
