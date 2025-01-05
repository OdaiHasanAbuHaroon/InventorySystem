using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Core
{
    /// <summary>
    /// Controller for managing client control panel operations such as handling security groups and feature permissions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientControlPanelController : ControllerBaseExt
    {
        private readonly ILogger<ClientControlPanelController> _logService;
        private readonly IHelperService _helperService;
        private readonly IClientAssetsService _clientAssetsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientControlPanelController"/> class.
        /// </summary>
        /// <param name="logsService">Logger for tracking operations and errors.</param>
        /// <param name="helperService">Helper service for utility and error handling.</param>
        /// <param name="clientAssetsService">Service to handle client asset-related business logic.</param>
        public ClientControlPanelController(
            ILogger<ClientControlPanelController> logsService,
            IHelperService helperService,
            IClientAssetsService clientAssetsService)
        {
            _logService = logsService;
            _helperService = helperService;
            _clientAssetsService = clientAssetsService;
        }

        #region SecurityGroup

        /// <summary>
        /// Retrieves a list of security groups for the client.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="SecurityGroupDataModel"/>.
        /// </returns>
        [HttpGet("ListSecurityGroup")]
        public async Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup()
        {
            try
            {
                return await _clientAssetsService.ListSecurityGroup(GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<SecurityGroupDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Adds a new security group for the client.
        /// </summary>
        /// <param name="form">The form data for creating a security group.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing the created <see cref="SecurityGroupDataModel"/>.
        /// </returns>
        [HttpPost("AddSecurityGroup")]
        public async Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form)
        {
            try
            {
                return await _clientAssetsService.AddSecurityGroup(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<SecurityGroupDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a security group by its ID.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.
        /// </returns>
        [HttpDelete("DeleteSecurityGroup")]
        public async Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId)
        {
            try
            {
                return await _clientAssetsService.DeleteSecurityGroup(securityGroupId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves a list of feature permissions available for assignment.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> containing a list of <see cref="FeaturePermissionDataModel"/>.
        /// </returns>
        [HttpGet("ListFeaturePermission")]
        public async Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission()
        {
            try
            {
                return await _clientAssetsService.ListFeaturePermission(GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<FeaturePermissionDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a feature permission to a security group.
        /// </summary>
        /// <param name="securityGroupId">The ID of the security group.</param>
        /// <param name="featurePermissionId">The ID of the feature permission to assign.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the assignment was successful.
        /// </returns>
        [HttpPost("AssignFeaturePermissionToSecurityGroup")]
        public async Task<GenericResponse<bool>> AssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId)
        {
            try
            {
                return await _clientAssetsService.AssignFeaturePermissionToSecurityGroup(securityGroupId, featurePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Unassigns a feature permission from a security group.
        /// </summary>
        /// <param name="securityGroupId">The ID of the security group.</param>
        /// <param name="featurePermissionId">The ID of the feature permission to unassign.</param>
        /// <returns>
        /// A <see cref="GenericResponse{T}"/> indicating whether the unassignment was successful.
        /// </returns>
        [HttpPost("UnAssignFeaturePermissionToSecurityGroup")]
        public async Task<GenericResponse<bool>> UnAssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId)
        {
            try
            {
                return await _clientAssetsService.UnAssignFeaturePermissionToSecurityGroup(securityGroupId, featurePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        #endregion
    }
}
