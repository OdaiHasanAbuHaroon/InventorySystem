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
    /// Controller for managing location-related operations such as listing, fetching, creating, updating, and deleting locations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBaseExt
    {
        private readonly ILogger<LocationController> _logService;
        private readonly ILocationService _locationService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for location-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public LocationController(ILogger<LocationController> logger, ILocationService service, IHelperService helperService)
        {
            _logService = logger;
            _locationService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of locations based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching locations.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="LocationDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("LocationList")]
        public async Task<GenericResponse<List<LocationDataModel>>> LocationList(LocationFilterModel filterModel)
        {
            try
            {
                return await _locationService.LocationList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<LocationDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific location by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the location.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="LocationDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetLocation")]
        public async Task<GenericResponse<LocationDataModel>> GetLocation(long Id)
        {
            try
            {
                return await _locationService.GetLocation(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a location by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the location to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteLocation")]
        public async Task<GenericResponse<bool>> DeleteLocation(long Id)
        {
            try
            {
                return await _locationService.DeleteLocation(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific location.
        /// </summary>
        /// <param name="id">The unique identifier of the location to be updated.</param>
        /// <param name="form">The updated location details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="LocationDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateLocation")]
        public async Task<GenericResponse<LocationDataModel>> UpdateLocation(long id, LocationFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _locationService.UpdateLocation(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new location using the provided form model.
        /// </summary>
        /// <param name="form">The location details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="LocationDataModel"/>.
        /// </returns>
        [HttpPost("CreateLocation")]
        public async Task<GenericResponse<LocationDataModel>> CreateLocation(LocationFormModel form)
        {
            try
            {
                return await _locationService.CreateLocation(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo locations for testing purposes and retrieves a list of all locations.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="LocationDataModel"/> objects, including the newly created demo locations.
        /// </returns>
        [HttpGet("RandomLocations")]
        public async Task<GenericResponse<List<LocationDataModel>>> RandomLocations()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomLocation = new LocationFormModel()
                    {
                        Name = $"Location{i}",
                        Type = $"Type for Location{i}",
                        Address = $"Location Address {i}",
                        ParentLocationId = null,
                    };

                    await _locationService.CreateLocation(randomLocation, GetLanguage());
                }

                return await _locationService.LocationList(new LocationFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<LocationDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
