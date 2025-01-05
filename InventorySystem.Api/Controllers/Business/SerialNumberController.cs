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
    /// Controller for managing serialNumber-related operations such as listing, fetching, creating, updating, and deleting serialNumbers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SerialNumberController : ControllerBaseExt
    {
        private readonly ILogger<SerialNumberController> _logService;
        private readonly ISerialNumberService _serialNumberService;
        private readonly IItemService _itemService;
        private readonly ILocationService _locationService;
        private readonly IItemStatusService _itemStatusService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialNumberController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for serialNumber-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public SerialNumberController(ILogger<SerialNumberController> logger, ISerialNumberService service, IHelperService helperService, IItemService itemService, ILocationService locationService, IItemStatusService itemStatusService)
        {
            _logService = logger;
            _serialNumberService = service;
            _helpService = helperService;
            _itemService = itemService;
            _locationService = locationService;
            _itemStatusService = itemStatusService;
        }

        /// <summary>
        /// Retrieves a list of serialNumbers based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching serialNumbers.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="SerialNumberDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("SerialNumberList")]
        public async Task<GenericResponse<List<SerialNumberDataModel>>> SerialNumberList(SerialNumberFilterModel filterModel)
        {
            try
            {
                return await _serialNumberService.SerialNumberList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<SerialNumberDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific serialNumber by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the serialNumber.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="SerialNumberDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetSerialNumber")]
        public async Task<GenericResponse<SerialNumberDataModel>> GetSerialNumber(long Id)
        {
            try
            {
                return await _serialNumberService.GetSerialNumber(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a serialNumber by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the serialNumber to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteSerialNumber")]
        public async Task<GenericResponse<bool>> DeleteSerialNumber(long Id)
        {
            try
            {
                return await _serialNumberService.DeleteSerialNumber(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific serialNumber.
        /// </summary>
        /// <param name="id">The unique identifier of the serialNumber to be updated.</param>
        /// <param name="form">The updated serialNumber details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="SerialNumberDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateSerialNumber")]
        public async Task<GenericResponse<SerialNumberDataModel>> UpdateSerialNumber(long id, SerialNumberFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _serialNumberService.UpdateSerialNumber(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new serialNumber using the provided form model.
        /// </summary>
        /// <param name="form">The serialNumber details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="SerialNumberDataModel"/>.
        /// </returns>
        [HttpPost("CreateSerialNumber")]
        public async Task<GenericResponse<SerialNumberDataModel>> CreateSerialNumber(SerialNumberFormModel form)
        {
            try
            {
                return await _serialNumberService.CreateSerialNumber(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SerialNumberDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo serialNumber s for testing purposes and retrieves a list of all serialNumber s.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="SerialNumberDataModel"/> objects, including the newly created demo serialNumber s.
        /// </returns>
        [HttpGet("RandomSerialNumbers")]
        public async Task<GenericResponse<List<SerialNumberDataModel>>> RandomSerialNumbers()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var item = _itemService.GetRandomItem(GetLanguage());
                    var location = _locationService.GetRandomLocation(GetLanguage());
                    var serialNumberStatus = _itemStatusService.GetRandomItemStatus(GetLanguage());

                    var randomSerialNumber = new SerialNumberFormModel()
                    {
                        Serial = $"SerialNumber{i}",
                        ItemId = item?.Id ?? 0,
                        Item = item,
                    };

                    await _serialNumberService.CreateSerialNumber(randomSerialNumber, GetLanguage());
                }

                return await _serialNumberService.SerialNumberList(new SerialNumberFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<SerialNumberDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
