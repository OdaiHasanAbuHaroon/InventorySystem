using InventorySystem.Mappers;
using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    /// <summary>
    /// Provides services for managing client assets including security groups and feature permissions.
    /// </summary>
    public class ClientAssetsService : IIScoped, IClientAssetsService
    {
        private readonly ILogger<ClientAssetsService> _logService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _httpContextDataProvider;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IAssetsService _assetsService;
        private readonly MapperService Mappers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientAssetsService"/> class.
        /// </summary>
        public ClientAssetsService(ILogger<ClientAssetsService> logService, IUnitOfWork unitOfWork, IHelperService helperService, IHttpContextDataProvider httpContextDataProvider, IHostEnvironment hostEnvironment, IAssetsService assetsService)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
            _helpService = helperService;
            _httpContextDataProvider = httpContextDataProvider;
            _hostEnvironment = hostEnvironment;
            _assetsService = assetsService;
            Mappers = _unitOfWork.MapperService;
        }

        #region SecurityGroup

        /// <summary>
        /// Retrieves a list of active security groups.
        /// </summary>
        public async Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup(string lang)
        {
            try
            {
                var UserInfo = _httpContextDataProvider.GetContextUserInfo();
                if (UserInfo == null)
                    return _helpService.CreateErrorResponse<List<SecurityGroupDataModel>>(MessageKeys.system_error, lang);

                var result = await _unitOfWork.SecurityGroupRepository.GetListAsync(x => x.IsActive);
                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, Mappers.SecurityGroupMapper.MapCollectionToDataModel(result), result.Count);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<SecurityGroupDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Adds a new security group.
        /// </summary>
        public async Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form, string lang)
        {
            var UserInfo = _httpContextDataProvider.GetContextUserInfo();
            if (UserInfo == null)
                return _helpService.CreateErrorResponse<SecurityGroupDataModel>(MessageKeys.system_error, lang);

            return await _assetsService.AddSecurityGroup(form, lang);
        }

        /// <summary>
        /// Deletes a security group by its ID.
        /// </summary>
        public async Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId, string lang)
        {
            return await _assetsService.DeleteSecurityGroup(securityGroupId, lang);
        }

        /// <summary>
        /// Retrieves a list of feature permissions.
        /// </summary>
        public async Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission(string lang)
        {
            try
            {
                var result = await _unitOfWork.Context.FeaturePermissions.AsNoTracking().ToListAsync();
                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, Mappers.FeaturePermissionMapper.MapCollectionToDataModel(result), result.Count);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<List<FeaturePermissionDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Assigns a feature permission to a security group.
        /// </summary>
        public async Task<GenericResponse<bool>> AssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang)
        {
            try
            {
                if (!await _unitOfWork.Context.GroupFeaturePermissions.AnyAsync(x => x.SecurityGroupId == securityGroupId && x.FeaturePermissionId == featurePermissionId))
                {
                    await _unitOfWork.Context.AddAsync(new GroupFeaturePermission() { FeaturePermissionId = featurePermissionId, SecurityGroupId = securityGroupId });
                    await _unitOfWork.CompleteAsync();
                    return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
                }
                else
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_exist, lang);
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Unassigns a feature permission from a security group.
        /// </summary>
        public async Task<GenericResponse<bool>> UnAssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang)
        {
            try
            {
                var current = await _unitOfWork.Context.GroupFeaturePermissions.FirstOrDefaultAsync(x => x.SecurityGroupId == securityGroupId && x.FeaturePermissionId == featurePermissionId);
                if (current != null)
                {
                    _unitOfWork.Context.GroupFeaturePermissions.Remove(current);
                    await _unitOfWork.CompleteAsync();
                    return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
                }
                else
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);
                }
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        #endregion
    }
}
