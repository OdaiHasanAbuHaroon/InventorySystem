using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Business;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Shared.Tools;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Business
{
    /// <summary>
    /// Controller for managing manufacturer-related operations such as listing, fetching, creating, updating, and deleting manufacturers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManufacturerController : ControllerBaseExt
    {
        private readonly ILogger<ManufacturerController> _logService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICountryService _countryService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for manufacturer-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerService service, IHelperService helperService, ICountryService countryService)
        {
            _logService = logger;
            _manufacturerService = service;
            _helpService = helperService;
            _countryService = countryService;
        }

        /// <summary>
        /// Retrieves a list of manufacturers based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching manufacturers.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="ManufacturerDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("ManufacturerList")]
        public async Task<GenericResponse<List<ManufacturerDataModel>>> ManufacturerList(ManufacturerFilterModel filterModel)
        {
            try
            {
                return await _manufacturerService.ManufacturerList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ManufacturerDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific manufacturer by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the manufacturer.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="ManufacturerDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetManufacturer")]
        public async Task<GenericResponse<ManufacturerDataModel>> GetManufacturer(long Id)
        {
            try
            {
                return await _manufacturerService.GetManufacturer(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a manufacturer by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the manufacturer to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteManufacturer")]
        public async Task<GenericResponse<bool>> DeleteManufacturer(long Id)
        {
            try
            {
                return await _manufacturerService.DeleteManufacturer(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific manufacturer.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer to be updated.</param>
        /// <param name="form">The updated manufacturer details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="ManufacturerDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateManufacturer")]
        public async Task<GenericResponse<ManufacturerDataModel>> UpdateManufacturer(long id, ManufacturerFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _manufacturerService.UpdateManufacturer(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new manufacturer using the provided form model.
        /// </summary>
        /// <param name="form">The manufacturer details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="ManufacturerDataModel"/>.
        /// </returns>
        [HttpPost("CreateManufacturer")]
        public async Task<GenericResponse<ManufacturerDataModel>> CreateManufacturer(ManufacturerFormModel form)
        {
            try
            {
                return await _manufacturerService.CreateManufacturer(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo manufacturers for testing purposes and retrieves a list of all manufacturers.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="ManufacturerDataModel"/> objects, including the newly created demo manufacturers.
        /// </returns>
        [HttpGet("RandomManufacturers")]
        public async Task<GenericResponse<List<ManufacturerDataModel>>> RandomManufacturers()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomManufacturer = new ManufacturerFormModel()
                    {
                        Name = $"Manufacturer{i}",
                        ContactName = $"ContactName for Manufacturer{i}",
                        ContactNumber = DemoDataUtility.GetRandomPhone(),
                        ContactEmail = $"manufacturer{i}@example.com",
                        Address = $"Manufacturer Address {i}",
                        Description = $"Description for Manufacturer {i}",
                        Country = _countryService.GetRandomCountry(GetLanguage()),
                    };

                    await _manufacturerService.CreateManufacturer(randomManufacturer, GetLanguage());
                }

                return await _manufacturerService.ManufacturerList(new ManufacturerFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<ManufacturerDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
