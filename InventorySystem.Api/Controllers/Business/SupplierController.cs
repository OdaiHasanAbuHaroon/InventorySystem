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
    /// Controller for managing supplier-related operations such as listing, fetching, creating, updating, and deleting suppliers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBaseExt
    {
        private readonly ILogger<SupplierController> _logService;
        private readonly ISupplierService _supplierService;
        private readonly ICountryService _countryService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for supplier-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public SupplierController(ILogger<SupplierController> logger, ISupplierService service, IHelperService helperService, ICountryService countryService)
        {
            _logService = logger;
            _supplierService = service;
            _helpService = helperService;
            _countryService = countryService;
        }

        /// <summary>
        /// Retrieves a list of suppliers based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching suppliers.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="SupplierDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("SupplierList")]
        public async Task<GenericResponse<List<SupplierDataModel>>> SupplierList(SupplierFilterModel filterModel)
        {
            try
            {
                return await _supplierService.SupplierList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<SupplierDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific supplier by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the supplier.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="SupplierDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetSupplier")]
        public async Task<GenericResponse<SupplierDataModel>> GetSupplier(long Id)
        {
            try
            {
                return await _supplierService.GetSupplier(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a supplier by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the supplier to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteSupplier")]
        public async Task<GenericResponse<bool>> DeleteSupplier(long Id)
        {
            try
            {
                return await _supplierService.DeleteSupplier(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific supplier.
        /// </summary>
        /// <param name="id">The unique identifier of the supplier to be updated.</param>
        /// <param name="form">The updated supplier details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="SupplierDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateSupplier")]
        public async Task<GenericResponse<SupplierDataModel>> UpdateSupplier(long id, SupplierFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _supplierService.UpdateSupplier(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new supplier using the provided form model.
        /// </summary>
        /// <param name="form">The supplier details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="SupplierDataModel"/>.
        /// </returns>
        [HttpPost("CreateSupplier")]
        public async Task<GenericResponse<SupplierDataModel>> CreateSupplier(SupplierFormModel form)
        {
            try
            {
                return await _supplierService.CreateSupplier(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo suppliers for testing purposes and retrieves a list of all suppliers.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="SupplierDataModel"/> objects, including the newly created demo suppliers.
        /// </returns>
        [HttpGet("RandomSuppliers")]
        public async Task<GenericResponse<List<SupplierDataModel>>> RandomSuppliers()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomSupplier = new SupplierFormModel()
                    {
                        Name = $"Supplier{i}",
                        ContactName = $"ContactName for Supplier{i}",
                        ContactNumber = DemoDataUtility.GetRandomPhone(),
                        ContactEmail = $"supplier{i}@example.com",
                        Address = $"Supplier Address {i}",
                        Description = $"Description for Supplier {i}",
                        Country = _countryService.GetRandomCountry(GetLanguage()),
                    };

                    await _supplierService.CreateSupplier(randomSupplier, GetLanguage());
                }

                return await _supplierService.SupplierList(new SupplierFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<SupplierDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
