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
    /// Provides services for managing manufacturers, including CRUD operations and validation.
    /// </summary>
    public class ManufacturerService : IIScoped, IManufacturerService
    {
        private readonly ILogger<ManufacturerService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public ManufacturerService(ILogger<ManufacturerService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a manufacturer with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The manufacturer form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the manufacturer exists; otherwise, false.</returns>
        public async Task<bool> ManufacturerExists(ManufacturerFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.ManufacturerRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of manufacturers based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for manufacturer data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of manufacturer data models.</returns>
        public async Task<GenericResponse<List<ManufacturerDataModel>>> ManufacturerList(ManufacturerFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.ManufacturerRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ManufacturerMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<ManufacturerDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new manufacturer record in the database.
        /// </summary>
        /// <param name="form">The manufacturer form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created manufacturer data model.</returns>
        public async Task<GenericResponse<ManufacturerDataModel>> CreateManufacturer(ManufacturerFormModel form, string lang)
        {
            try
            {
                if (await ManufacturerExists(form))
                    return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.ManufacturerMapper.MapDataFormToEntity(form);
                await _unitOfWork.ManufacturerRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ManufacturerMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing manufacturer record in the database.
        /// </summary>
        /// <param name="form">The manufacturer form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated manufacturer data model.</returns>
        public async Task<GenericResponse<ManufacturerDataModel>> UpdateManufacturer(ManufacturerFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.ManufacturerRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.record_notfound, lang);

                if (await ManufacturerExists(form, true))
                    return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.ManufacturerMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.ManufacturerRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ManufacturerMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing manufacturer record from the database.
        /// </summary>
        /// <param name="id">The ID of the manufacturer to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteManufacturer(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ManufacturerRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.ManufacturerRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a manufacturer by its ID.
        /// </summary>
        /// <param name="id">The ID of the manufacturer to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the manufacturer data model.</returns>
        public async Task<GenericResponse<ManufacturerDataModel>> GetManufacturer(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.ManufacturerRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.ManufacturerMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<ManufacturerDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random manufacturer's details.
        /// </summary>
        public ManufacturerFormModel? GetRandomManufacturer(string lang)
        {
            ManufacturerFilterModel filterModel = new();
            List<Manufacturer> allManufacturers = _unitOfWork.ManufacturerRepository.GetAll().Result.ToList();

            if (allManufacturers != null && allManufacturers.Count > 0)
            {
                int randomIndex = new Random().Next(allManufacturers.Count);

                return _mapperService.ManufacturerMapper.MapEntityToDataForm(allManufacturers[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
