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
    /// Controller for managing itemStatus-related operations such as listing, fetching, creating, updating, and deleting itemStatuss.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemStatusController : ControllerBaseExt
    {
        private readonly ILogger<ItemStatusController> _logService;
        private readonly IItemStatusService _itemStatusService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStatusController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for itemStatus-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public ItemStatusController(ILogger<ItemStatusController> logger, IItemStatusService service, IHelperService helperService)
        {
            _logService = logger;
            _itemStatusService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of itemStatuss based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching itemStatuss.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="ItemStatusDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("ItemStatusList")]
        public async Task<GenericResponse<List<ItemStatusDataModel>>> ItemStatusList(ItemStatusFilterModel filterModel)
        {
            try
            {
                return await _itemStatusService.ItemStatusList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ItemStatusDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific itemStatus by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the itemStatus.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="ItemStatusDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetItemStatus")]
        public async Task<GenericResponse<ItemStatusDataModel>> GetItemStatus(long Id)
        {
            try
            {
                return await _itemStatusService.GetItemStatus(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a itemStatus by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the itemStatus to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteItemStatus")]
        public async Task<GenericResponse<bool>> DeleteItemStatus(long Id)
        {
            try
            {
                return await _itemStatusService.DeleteItemStatus(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific itemStatus.
        /// </summary>
        /// <param name="id">The unique identifier of the itemStatus to be updated.</param>
        /// <param name="form">The updated itemStatus details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="ItemStatusDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateItemStatus")]
        public async Task<GenericResponse<ItemStatusDataModel>> UpdateItemStatus(long id, ItemStatusFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _itemStatusService.UpdateItemStatus(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new itemStatus using the provided form model.
        /// </summary>
        /// <param name="form">The itemStatus details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="ItemStatusDataModel"/>.
        /// </returns>
        [HttpPost("CreateItemStatus")]
        public async Task<GenericResponse<ItemStatusDataModel>> CreateItemStatus(ItemStatusFormModel form)
        {
            try
            {
                return await _itemStatusService.CreateItemStatus(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ItemStatusDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo itemStatuss for testing purposes and retrieves a list of all itemStatuss.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="ItemStatusDataModel"/> objects, including the newly created demo itemStatuss.
        /// </returns>
        [HttpGet("RandomItemStatuses")]
        public async Task<GenericResponse<List<ItemStatusDataModel>>> RandomItemStatuses()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomItemStatus = new ItemStatusFormModel()
                    {
                        Name = $"ItemStatus{i}",
                        Description = $"Description for ItemStatus {i}"
                    };

                    await _itemStatusService.CreateItemStatus(randomItemStatus, GetLanguage());
                }

                return await _itemStatusService.ItemStatusList(new ItemStatusFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<ItemStatusDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
