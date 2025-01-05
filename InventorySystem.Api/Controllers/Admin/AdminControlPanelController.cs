using InventorySystem.Api.Extentions;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.Shared.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InventorySystem.Api.Controllers.Admin
{
    /// <summary>
    /// A controller that provides administrative functionalities, such as user management, role assignments, module handling,
    /// and security group management for the system.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminControlPanelController : ControllerBaseExt
    {
        private readonly ILogger<AdminControlPanelController> _logService;
        private readonly ICustomMessageProvider _messageProvider;
        private readonly IHelperService _helperService;
        private readonly IAssetsService _assetsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminControlPanelController"/> class.
        /// </summary>
        /// <param name="logsService">An <see cref="ILogger"/> instance for logging activities.</param>
        /// <param name="messageProvider">A <see cref="ICustomMessageProvider"/> instance for retrieving localized messages.</param>
        /// <param name="helperService">A <see cref="IHelperService"/> instance for creating and managing responses.</param>
        /// <param name="assetsService">An <see cref="IAssetsService"/> instance for handling user-related operations.</param>
        public AdminControlPanelController(ILogger<AdminControlPanelController> logsService, ICustomMessageProvider messageProvider, IHelperService helperService, IAssetsService assetsService)
        {
            _logService = logsService;
            _messageProvider = messageProvider;
            _helperService = helperService;
            _assetsService = assetsService;
        }

        #region SecurityGroup

        /// <summary>
        /// Retrieves a list of all security groups available in the system.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="SecurityGroupDataModel"/> objects representing the security groups.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpGet("ListSecurityGroup")]
        public async Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup()
        {
            try
            {
                return await _assetsService.ListSecurityGroup(GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<SecurityGroupDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Adds a new security group to the system.
        /// </summary>
        /// <param name="form">A <see cref="SecurityGroupFormModel"/> containing the details of the security group to be added.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing the details of the newly created security group as a <see cref="SecurityGroupDataModel"/>.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AddSecurityGroup")]
        public async Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form)
        {
            try
            {
                return await _assetsService.AddSecurityGroup(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<SecurityGroupDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a security group from the system by its unique identifier.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group to delete.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpDelete("DeleteSecurityGroup")]
        public async Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId)
        {
            try
            {
                return await _assetsService.DeleteSecurityGroup(securityGroupId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves a list of all feature permissions available in the system.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="FeaturePermissionDataModel"/> objects representing the feature permissions.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpGet("ListFeaturePermission")]
        public async Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission()
        {
            try
            {
                return await _assetsService.ListFeaturePermission(GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<FeaturePermissionDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a feature permission to a specific security group in the system.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group to which the feature permission will be assigned.</param>
        /// <param name="featurePermissionId">The unique identifier of the feature permission to assign.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AssignFeaturePermissionToSecurityGroup")]
        public async Task<GenericResponse<bool>> AssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId)
        {
            try
            {
                return await _assetsService.AssignFeaturePermissionToSecurityGroup(securityGroupId, featurePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Removes a feature permission from a security group.
        /// </summary>
        /// <param name="securityGroupId">The ID of the security group to remove the feature permission from.</param>
        /// <param name="featurePermissionId">The ID of the feature permission to remove.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnAssignFeaturePermissionToSecurityGroup")]
        public async Task<GenericResponse<bool>> UnAssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId)
        {
            try
            {
                return await _assetsService.UnAssignFeaturePermissionToSecurityGroup(securityGroupId, featurePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        #endregion

        #region User

        /// <summary>
        /// Retrieves a list of users based on the specified filter criteria.
        /// </summary>
        /// <param name="filterModel">The filter model containing criteria for querying users.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="UserDataModel"/> objects that match the filter criteria.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("ListUsers")]
        public async Task<GenericResponse<List<UserDataModel>>> ListUsers(UserFilterModel filterModel)
        {
            try
            {
                return await _assetsService.ListUsers(filterModel, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<UserDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Retrieves the details of a user by their unique identifier.
        /// </summary>
        /// <param name="Id">The unique identifier of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing the details of the user as a <see cref="UserDataModel"/>.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpGet("GetUser")]
        public async Task<GenericResponse<UserDataModel>> GetUser(long Id)
        {
            try
            {
                return await _assetsService.GetUser(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Adds a new user to the system based on the provided user details.
        /// </summary>
        /// <param name="form">A <see cref="UserFormModel"/> containing the details of the user to be added.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing the details of the newly created user as a <see cref="UserDataModel"/>.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AddUser")]
        public async Task<GenericResponse<UserDataModel>> AddUser(UserFormModel form)
        {
            try
            {
                return await _assetsService.AddUser(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates an existing user in the system with the provided details.
        /// </summary>
        /// <param name="Id">The unique identifier of the user to be updated.</param>
        /// <param name="form">A <see cref="UserFormModel"/> containing the updated details of the user.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing the updated user details as a <see cref="UserDataModel"/>.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPut("UpdateUser")]
        public async Task<GenericResponse<UserDataModel>> UpdateUser(long Id, UserFormModel form)
        {
            try
            {
                if (Id != form.Id)
                {
                    return _helperService.CreateErrorResponse<UserDataModel>(MessageKeys.invalid_parameter, GetLanguage());
                }
                return await _assetsService.UpdateUser(form, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Deletes a user from the system by their unique identifier.
        /// </summary>
        /// <param name="Id">The unique identifier of the user to be deleted.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpDelete("DeleteUser")]
        public async Task<GenericResponse<bool>> DeleteUser(long Id)
        {
            try
            {
                return await _assetsService.DeleteUser(Id, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Unlocks a user account by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be unlocked.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnlockUser")]
        public async Task<GenericResponse<bool>> UnlockUser(long userId)
        {
            try
            {
                return await _assetsService.UnlockUser(userId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Updates the active status of a user in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose status is being updated.</param>
        /// <param name="status">The new status to assign to the user (e.g., active or inactive).</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UpdateUserStatus")]
        public async Task<GenericResponse<bool>> UpdateUserStatus(long userId, bool status)
        {
            try
            {
                return await _assetsService.UpdateUserStatus(userId, status, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a user to a specific module in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to assign.</param>
        /// <param name="moduleId">The unique identifier of the module to assign the user to.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AssignUserToModule")]
        public async Task<GenericResponse<bool>> AssignUserToModule(long userId, long moduleId)
        {
            try
            {
                return await _assetsService.AssignUserToModule(userId, moduleId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a user to a specific role in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to assign.</param>
        /// <param name="roleId">The unique identifier of the role to assign the user to.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AssignUserToRole")]
        public async Task<GenericResponse<bool>> AssignUserToRole(long userId, long roleId)
        {
            try
            {
                return await _assetsService.AssignUserToRole(userId, roleId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a user to a specific security group in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to assign.</param>
        /// <param name="securityGroupId">The unique identifier of the security group to assign the user to.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AssignUserToSecurityGroup")]
        public async Task<GenericResponse<bool>> AssignUserToSecurityGroup(long userId, long securityGroupId)
        {
            try
            {
                return await _assetsService.AssignUserToSecurityGroup(userId, securityGroupId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Assigns a user to a specific feature permission in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to assign.</param>
        /// <param name="FeaturePermissionId">The unique identifier of the feature permission to assign the user to.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("AssignUserToFeaturePermission")]
        public async Task<GenericResponse<bool>> AssignUserToFeaturePermission(long userId, long FeaturePermissionId)
        {
            try
            {
                return await _assetsService.AssignUserToFeaturePermission(userId, FeaturePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Removes a user's assignment from a specific module in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to unassign.</param>
        /// <param name="moduleId">The unique identifier of the module to remove the user from.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnAssignUserFromModule")]
        public async Task<GenericResponse<bool>> UnAssignUserFromModule(long userId, long moduleId)
        {
            try
            {
                return await _assetsService.UnAssignUserFromModule(userId, moduleId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Removes a user's assignment from a specific role in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to unassign.</param>
        /// <param name="roleId">The unique identifier of the role to remove the user from.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnAssignUserFromRole")]
        public async Task<GenericResponse<bool>> UnAssignUserFromRole(long userId, long roleId)
        {
            try
            {
                return await _assetsService.UnAssignUserFromRole(userId, roleId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Removes a user's assignment from a specific security group in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to unassign.</param>
        /// <param name="securityGroupId">The unique identifier of the security group to remove the user from.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnAssignUserFromSecurityGroup")]
        public async Task<GenericResponse<bool>> UnAssignUserFromSecurityGroup(long userId, long securityGroupId)
        {
            try
            {
                return await _assetsService.UnAssignUserFromSecurityGroup(userId, securityGroupId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Removes a user's assignment from a specific feature permission in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to unassign.</param>
        /// <param name="FeaturePermissionId">The unique identifier of the feature permission to remove the user from.</param>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a boolean value indicating whether the operation was successful.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpPost("UnAssignUserFromFeaturePermission")]
        public async Task<GenericResponse<bool>> UnAssignUserFromFeaturePermission(long userId, long FeaturePermissionId)
        {
            try
            {
                return await _assetsService.UnAssignUserFromFeaturePermission(userId, FeaturePermissionId, GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<bool>(MessageKeys.system_error, GetLanguage());
            }
        }

        /// <summary>
        /// Generates 10 random demo users for testing purposes and retrieves a list of all users.
        /// </summary>
        /// <returns>
        /// A <see cref="GenericResponse"/> containing a list of <see cref="UserDataModel"/> objects, including the newly created demo users.
        /// </returns>
        [Authorize(Roles = $"{RoleDefinitions.SuperAdministrator}"), HttpGet("RandomUsers")]
        public async Task<GenericResponse<List<UserDataModel>>> RandomUsers()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var randomUser = new UserFormModel()
                    {
                        Email = $"user{i}@gmail.com",
                        EmailConfirmed = true,
                        FirstName = $"user{i}",
                        LastName = $"user{i}",
                        LookoutEnabled = true,
                        MobileNumberConfirmed = true,
                        PhoneNumber = DemoDataUtility.GetRandomPhone(),
                        SmsEnabled = true,
                        TwoFactorEnabled = true,
                        Password = "123456789",
                        MiddleName = "*",
                        Image = new AttachmentFormModel()
                        {
                            Extention = "png",
                            FileContent = DemoDataUtility.GetRandomImage(),
                            Name = $"img{i}",
                            Path = "*"
                        },
                        DateOfBirth = new DateTime(1991, 9, 6),
                        Gender = "Male",
                        LanguageId = 1,
                        ThemeId = 1,
                        TimeZone_InfoId = 1,
                        Address = "Ideal Solutions",
                        UserFontSize = 18,
                        UserExtraInfo = new Shared.DTOs.Identity.UserExtraInfo()
                        {
                            DateOfBirth = DateTime.UtcNow,
                            Gender = "Male",
                            LanguageId = DemoDataUtility.GetRandomId(),
                            ThemeId = 1,
                            TimeZone_InfoId = DemoDataUtility.GetRandomId(),
                            UserFontSize = 12,
                            Address = $"Address{i}"
                        }
                    };
                    await _assetsService.AddUser(randomUser, GetLanguage());
                }
                return await _assetsService.ListUsers(new UserFilterModel(), GetLanguage());
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return _helperService.CreateErrorResponse<List<UserDataModel>>(MessageKeys.system_error, GetLanguage());
            }
        }

        #endregion
    }
}
