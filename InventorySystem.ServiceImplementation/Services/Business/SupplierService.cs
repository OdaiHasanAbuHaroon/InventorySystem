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
    /// Provides services for managing suppliers, including CRUD operations and validation.
    /// </summary>
    public class SupplierService : IIScoped, ISupplierService
    {
        private readonly ILogger<SupplierService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public SupplierService(ILogger<SupplierService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a supplier with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The supplier form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the supplier exists; otherwise, false.</returns>
        public async Task<bool> SupplierExists(SupplierFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.SupplierRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of suppliers based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for supplier data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of supplier data models.</returns>
        public async Task<GenericResponse<List<SupplierDataModel>>> SupplierList(SupplierFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.SupplierRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SupplierMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<SupplierDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new supplier record in the database.
        /// </summary>
        /// <param name="form">The supplier form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created supplier data model.</returns>
        public async Task<GenericResponse<SupplierDataModel>> CreateSupplier(SupplierFormModel form, string lang)
        {
            try
            {
                if (await SupplierExists(form))
                    return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.SupplierMapper.MapDataFormToEntity(form);
                await _unitOfWork.SupplierRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SupplierMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing supplier record in the database.
        /// </summary>
        /// <param name="form">The supplier form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated supplier data model.</returns>
        public async Task<GenericResponse<SupplierDataModel>> UpdateSupplier(SupplierFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.SupplierRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.record_notfound, lang);

                if (await SupplierExists(form, true))
                    return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.SupplierMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.SupplierRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SupplierMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing supplier record from the database.
        /// </summary>
        /// <param name="id">The ID of the supplier to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteSupplier(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.SupplierRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.SupplierRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a supplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the supplier data model.</returns>
        public async Task<GenericResponse<SupplierDataModel>> GetSupplier(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.SupplierRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SupplierMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<SupplierDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random supplier's details.
        /// </summary>
        public SupplierFormModel? GetRandomSupplier(string lang)
        {
            SupplierFilterModel filterModel = new();
            List<Supplier> allSuppliers = _unitOfWork.SupplierRepository.GetAll().Result.ToList();

            if (allSuppliers != null && allSuppliers.Count > 0)
            {
                int randomIndex = new Random().Next(allSuppliers.Count);

                return _mapperService.SupplierMapper.MapEntityToDataForm(allSuppliers[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
