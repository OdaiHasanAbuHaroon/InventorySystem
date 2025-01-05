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
    /// Provides services for managing locations, including CRUD operations and validation.
    /// </summary>
    public class LocationService : IIScoped, ILocationService
    {
        private readonly ILogger<LocationService> _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _contextDataProvider;
        private readonly MapperService _mapperService;

        public DbContext Context => throw new NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        /// <param name="logRepository">Logger for error and event logging.</param>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="helperRepository">Helper service for creating standardized responses.</param>
        /// <param name="connectionStringProvider">Provider for HTTP context data.</param>
        public LocationService(ILogger<LocationService> logRepository, IUnitOfWork unitOfWork, IHelperService helperRepository, IHttpContextDataProvider connectionStringProvider)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _helpService = helperRepository;
            _contextDataProvider = connectionStringProvider;
            _mapperService = _unitOfWork.MapperService;
        }

        /// <summary>
        /// Checks if a location with the given form data already exists in the database.
        /// </summary>
        /// <param name="form">The location form model containing the data to check.</param>
        /// <param name="checkDifferentId">Specifies whether to exclude a specific ID from the check.</param>
        /// <returns>True if the location exists; otherwise, false.</returns>
        public async Task<bool> LocationExists(LocationFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.LocationRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Retrieves a paginated list of locations based on the filter model.
        /// </summary>
        /// <param name="filterModel">The filter model for location data.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing a list of location data models.</returns>
        public async Task<GenericResponse<List<LocationDataModel>>> LocationList(LocationFilterModel filterModel, string lang)
        {
            try
            {
                var result = await (await _unitOfWork.LocationRepository.FilterByAsync(filterModel)).ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.LocationMapper.MapCollectionToDataModel(result), result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logRepository.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<LocationDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Creates a new location record in the database.
        /// </summary>
        /// <param name="form">The location form model containing the data to create.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the created location data model.</returns>
        public async Task<GenericResponse<LocationDataModel>> CreateLocation(LocationFormModel form, string lang)
        {
            try
            {
                if (await LocationExists(form))
                    return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.record_exist, lang);

                var newItem = _mapperService.LocationMapper.MapDataFormToEntity(form);
                await _unitOfWork.LocationRepository.InsertAsync(newItem, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.LocationMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Updates an existing location record in the database.
        /// </summary>
        /// <param name="form">The location form model containing the data to update.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the updated location data model.</returns>
        public async Task<GenericResponse<LocationDataModel>> UpdateLocation(LocationFormModel form, string lang)
        {
            try
            {
                var current = await _unitOfWork.LocationRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (current == null)
                    return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.record_notfound, lang);

                if (await LocationExists(form, true))
                    return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.record_exist, lang);

                current = _mapperService.LocationMapper.MapUpdateDataFormToEntity(form, current);
                await _unitOfWork.LocationRepository.UpdateAsync(current, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.LocationMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Deletes an existing location record from the database.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response indicating the success or failure of the delete operation.</returns>
        public async Task<GenericResponse<bool>> DeleteLocation(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.LocationRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);

                await _unitOfWork.LocationRepository.DeleteAsync(current);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <param name="lang">The language for response messages.</param>
        /// <returns>A response containing the location data model.</returns>
        public async Task<GenericResponse<LocationDataModel>> GetLocation(long id, string lang)
        {
            try
            {
                var current = await _unitOfWork.LocationRepository.GetAsync(x => x.Id == id);
                if (current == null)
                    return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.record_notfound, lang);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.LocationMapper.MapEntityToDataModel(current));
            }
            catch (Exception ex)
            {
                _logRepository.LogError(ex, ex.Message);
                return _helpService.CreateErrorResponse<LocationDataModel>(MessageKeys.system_error, lang, ex);
            }
        }

        /// <summary>
        /// Retrieves a random location's details.
        /// </summary>
        public LocationFormModel? GetRandomLocation(string lang)
        {
            LocationFilterModel filterModel = new();
            List<Location> allLocations = _unitOfWork.LocationRepository.GetAll().Result.ToList();

            if (allLocations != null && allLocations.Count > 0)
            {
                int randomIndex = new Random().Next(allLocations.Count);

                return _mapperService.LocationMapper.MapEntityToDataForm(allLocations[randomIndex]);
            }
            else
            {
                return null;
            }
        }
    }
}
