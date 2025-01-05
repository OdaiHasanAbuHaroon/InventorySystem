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
    /// Controller for managing brand-related operations such as listing, fetching, creating, updating, and deleting brands.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBaseExt
    {
        private readonly ILogger<BrandController> _logService;
        private readonly IBrandService _brandService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for brand-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public BrandController(ILogger<BrandController> logger, IBrandService service, IHelperService helperService, IManufacturerService manufacturerService)
        {
            _logService = logger;
            _brandService = service;
            _helpService = helperService;
            _manufacturerService = manufacturerService;
        }

        /// <summary>
        /// Retrieves a list of brands based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching brands.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="BrandDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("BrandList")]
        public async Task<GenericResponse<List<BrandDataModel>>> BrandList(BrandFilterModel filterModel)
        {
            try
            {
                return await _brandService.BrandList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<BrandDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific brand by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the brand.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="BrandDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetBrand")]
        public async Task<GenericResponse<BrandDataModel>> GetBrand(long Id)
        {
            try
            {
                return await _brandService.GetBrand(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a brand by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the brand to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteBrand")]
        public async Task<GenericResponse<bool>> DeleteBrand(long Id)
        {
            try
            {
                return await _brandService.DeleteBrand(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific brand.
        /// </summary>
        /// <param name="id">The unique identifier of the brand to be updated.</param>
        /// <param name="form">The updated brand details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="BrandDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateBrand")]
        public async Task<GenericResponse<BrandDataModel>> UpdateBrand(long id, BrandFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _brandService.UpdateBrand(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new brand using the provided form model.
        /// </summary>
        /// <param name="form">The brand details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="BrandDataModel"/>.
        /// </returns>
        [HttpPost("CreateBrand")]
        public async Task<GenericResponse<BrandDataModel>> CreateBrand(BrandFormModel form)
        {
            try
            {
                return await _brandService.CreateBrand(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo brands for testing purposes and retrieves a list of all brands.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="BrandDataModel"/> objects, including the newly created demo brands.
        /// </returns>
        [HttpGet("RandomBrands")]
        public async Task<GenericResponse<List<BrandDataModel>>> RandomBrands()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var manufacturer = _manufacturerService.GetRandomManufacturer(GetLanguage());
                    var randomBrand = new BrandFormModel()
                    {
                        Name = $"Brand{i}",
                        Description = $"Description for Brand{i}",
                        ManufacturerId = manufacturer?.Id ?? 0,
                        Manufacturer = manufacturer,
                    };

                    await _brandService.CreateBrand(randomBrand, GetLanguage());
                }

                return await _brandService.BrandList(new BrandFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<BrandDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
