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
    /// Controller for managing category-related operations such as listing, fetching, creating, updating, and deleting categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBaseExt
    {
        private readonly ILogger<CategoryController> _logService;
        private readonly ICategoryService _categoryService;
        private readonly IHelperService _helpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="service">Service to handle business logic for category-related operations.</param>
        /// <param name="helperService">Helper service for creating generic responses and utility functions.</param>
        public CategoryController(ILogger<CategoryController> logger, ICategoryService service, IHelperService helperService)
        {
            _logService = logger;
            _categoryService = service;
            _helpService = helperService;
        }

        /// <summary>
        /// Retrieves a list of categories based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for fetching categories.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="CategoryDataModel"/> that match the filter.
        /// </returns>
        [HttpPost("CategoryList")]
        public async Task<GenericResponse<List<CategoryDataModel>>> CategoryList(CategoryFilterModel filterModel)
        {
            try
            {
                return await _categoryService.CategoryList(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<CategoryDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves details of a specific category by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the category.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the <see cref="CategoryDataModel"/> for the specified ID.
        /// </returns>
        [HttpGet("GetCategory")]
        public async Task<GenericResponse<CategoryDataModel>> GetCategory(long Id)
        {
            try
            {
                return await _categoryService.GetCategory(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a category by their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the category to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteCategory")]
        public async Task<GenericResponse<bool>> DeleteCategory(long Id)
        {
            try
            {
                return await _categoryService.DeleteCategory(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the details of a specific category.
        /// </summary>
        /// <param name="id">The unique identifier of the category to be updated.</param>
        /// <param name="form">The updated category details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the updated <see cref="CategoryDataModel"/>.
        /// Returns an error response if the ID in the URL and the form do not match.
        /// </returns>
        [HttpPut("UpdateCategory")]
        public async Task<GenericResponse<CategoryDataModel>> UpdateCategory(long id, CategoryFormModel form)
        {
            try
            {
                if (id != form.Id)
                {
                    return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _categoryService.UpdateCategory(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Creates a new category using the provided form model.
        /// </summary>
        /// <param name="form">The category details in the form model.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="CategoryDataModel"/>.
        /// </returns>
        [HttpPost("CreateCategory")]
        public async Task<GenericResponse<CategoryDataModel>> CreateCategory(CategoryFormModel form)
        {
            try
            {
                return await _categoryService.CreateCategory(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo categories for testing purposes and retrieves a list of all categories.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="CategoryDataModel"/> objects, including the newly created demo categories.
        /// </returns>
        [HttpGet("RandomCategories")]
        public async Task<GenericResponse<List<CategoryDataModel>>> RandomCategories()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomCategory = new CategoryFormModel()
                    {
                        Name = $"Category{i}",
                        Description = $"Description for Category{i}",
                    };

                    await _categoryService.CreateCategory(randomCategory, GetLanguage());
                }

                return await _categoryService.CategoryList(new CategoryFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<CategoryDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }
    }
}
