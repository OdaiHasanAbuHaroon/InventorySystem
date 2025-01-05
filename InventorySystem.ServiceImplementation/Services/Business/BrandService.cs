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
    /// Provides services for managing brands, including CRUD operations and validation.
    /// </summary>
    public class BrandService : IIScoped, IBrandService
    {
        private readonly ILogger<BrandService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public BrandService(ILogger<BrandService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a brand with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The brand form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the brand exists; otherwise, false.</returns>
        public async Task<bool> BrandExists(BrandFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.BrandRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of brands based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for brand data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of brand data models.</returns>
        public async Task<GenericResponse<List<BrandDataModel>>> BrandList(BrandFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.BrandRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.BrandMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<BrandDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new brand record in the database.
        /// </summary>
        /// <param name="form">The brand form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created brand data model.</returns>
        public async Task<GenericResponse<BrandDataModel>> CreateBrand(BrandFormModel form, string lang)
        {
            try
            {
                if (await BrandExists(form))
                    return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.BrandMapper.MapDataFormToEntity(form);
                await _unitOfWork.BrandRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.BrandMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing brand record in the database.
        /// </summary>
        /// <param name="form">The brand form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated brand data model.</returns>
        public async Task<GenericResponse<BrandDataModel>> UpdateBrand(BrandFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.record_notfound, lang);

                if (await BrandExists(form, true))
                    return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.BrandMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.BrandRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.BrandMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing brand record from the database.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteBrand(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.BrandRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the brand data model.</returns>
        public async Task<GenericResponse<BrandDataModel>> GetBrand(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.BrandRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.BrandMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<BrandDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random brand's details.
        /// </summary>
        public BrandFormModel? GetRandomBrand(string lang)
        {
            BrandFilterModel filterModel = new();
            List<Brand> allBrands = _unitOfWork.BrandRepository.GetAll().Result.ToList();

            if (allBrands != null && allBrands.Count > 0)
            {
                int randomIndex = new Random().Next(allBrands.Count);

                return _mapperService.BrandMapper.MapEntityToDataForm(allBrands[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
