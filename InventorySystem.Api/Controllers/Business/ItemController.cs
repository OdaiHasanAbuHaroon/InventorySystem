using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Business;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Business
{
    /// <summary>
    /// Controller for managing item-related operations such as listing, fetching, creating, updating, and deleting items.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBaseExt
    {
        private readonly ILogger<ItemController> _logService;
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IItemStatusService _itemStatusService;
        private readonly ILocationService _locationService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for item-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public ItemController(ILogger<ItemController> logger, IItemService service, IHelperService helperService, ICategoryService categoryService, IBrandService brandService, IItemStatusService itemStatusService, ILocationService locationService)
        {
            _logService = logger;
            _itemService = service;
            _helpService = helperService;
            _categoryService = categoryService;
            _brandService = brandService;
            _itemStatusService = itemStatusService;
            _locationService = locationService;
        }

        /// <summary>
        /// Retrieves a list of items based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching items.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="ItemDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("ItemList")]
        public async Task<GenericResponse<List<ItemDataModel>>> ItemList(ItemFilterModel filterModel)
        {
            try
            {
                return await _itemService.ItemList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ItemDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific item by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the item.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="ItemDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetItem")]
        public async Task<GenericResponse<ItemDataModel>> GetItem(long Id)
        {
            try
            {
                return await _itemService.GetItem(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a item by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the item to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteItem")]
        public async Task<GenericResponse<bool>> DeleteItem(long Id)
        {
            try
            {
                return await _itemService.DeleteItem(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific item.
        /// </summary>
        /// <param name="id">The unique identifier of the item to be updated.</param>
        /// <param name="form">The updated item details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="ItemDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateItem")]
        public async Task<GenericResponse<ItemDataModel>> UpdateItem(long id, ItemFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _itemService.UpdateItem(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new item using the provided form model.
        /// </summary>
        /// <param name="form">The item details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="ItemDataModel"/>.
        /// </returns>
        [HttpPost("CreateItem")]
        public async Task<GenericResponse<ItemDataModel>> CreateItem(ItemFormModel form)
        {
            try
            {
                return await _itemService.CreateItem(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo item s for testing purposes and retrieves a list of all item s.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="ItemDataModel"/> objects, including the newly created demo item s.
        /// </returns>
        [HttpGet("RandomItems")]
        public async Task<GenericResponse<List<ItemDataModel>>> RandomItems()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var category = _categoryService.GetRandomCategory(GetLanguage());
                    var brand = _brandService.GetRandomBrand(GetLanguage());
                    var itemStatus = _itemStatusService.GetRandomItemStatus(GetLanguage());
                    var location = _locationService.GetRandomLocation(GetLanguage());

                    var randomItem = new ItemFormModel()
                    {
                        Name = $"Item{i}",
                        Description = $"Description for Item {i}",
                        UnitOfMeasurement = $"UnitOfMeasurement for Item {i}",
                        Serialized = (i % 2 == 1) ? true : false,
                        CategoryId = category?.Id ?? 0,
                        Category = category,
                        BrandId = brand?.Id ?? 0,
                        Brand = brand,
                        ItemStatusId = itemStatus?.Id ?? 0,
                        ItemStatus = itemStatus,
                        LocationId = location?.Id ?? 0,
                        Location = location,
                    };

                    await _itemService.CreateItem(randomItem, GetLanguage());
                }

                return await _itemService.ItemList(new ItemFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<ItemDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
