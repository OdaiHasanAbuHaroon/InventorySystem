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
    /// Provides services for managing categories, including CRUD operations and validation.
    /// </summary>
    public class CategoryService : IIScoped, ICategoryService
    {
        private readonly ILogger<CategoryService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public CategoryService(ILogger<CategoryService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a category with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The category form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the category exists; otherwise, false.</returns>
        public async Task<bool> CategoryExists(CategoryFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.CategoryRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of categories based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for category data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of category data models.</returns>
        public async Task<GenericResponse<List<CategoryDataModel>>> CategoryList(CategoryFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.CategoryRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CategoryMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<CategoryDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new category record in the database.
        /// </summary>
        /// <param name="form">The category form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created category data model.</returns>
        public async Task<GenericResponse<CategoryDataModel>> CreateCategory(CategoryFormModel form, string lang)
        {
            try
            {
                if (await CategoryExists(form))
                    return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.CategoryMapper.MapDataFormToEntity(form);
                await _unitOfWork.CategoryRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CategoryMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing category record in the database.
        /// </summary>
        /// <param name="form">The category form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated category data model.</returns>
        public async Task<GenericResponse<CategoryDataModel>> UpdateCategory(CategoryFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.record_notfound, lang);

                if (await CategoryExists(form, true))
                    return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.CategoryMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.CategoryRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CategoryMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing category record from the database.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteCategory(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.CategoryRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the category data model.</returns>
        public async Task<GenericResponse<CategoryDataModel>> GetCategory(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.CategoryMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<CategoryDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random category's details.
        /// </summary>
        public CategoryFormModel? GetRandomCategory(string lang)
        {
            CategoryFilterModel filterModel = new();
            List<Category> allCategories = _unitOfWork.CategoryRepository.GetAll().Result.ToList();

            if (allCategories != null && allCategories.Count > 0)
            {
                int randomIndex = new Random().Next(allCategories.Count);

                return _mapperService.CategoryMapper.MapEntityToDataForm(allCategories[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
