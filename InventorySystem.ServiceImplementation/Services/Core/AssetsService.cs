using InventorySystem.Mappers;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.Interfaces.Providers;
using InventorySystem.Shared.Interfaces.Services;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Shared.Messages;
using InventorySystem.Shared.Responses;
using InventorySystem.ServiceImplementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    /// <summary>
    /// Service for managing assets and security group operations including listing, adding, deleting, and permissions handling.
    /// </summary>
    public class AssetsService : IIScoped, IAssetsService
    {
        private readonly ILogger<AssetsService> _logService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelperService _helpService;
        private readonly IHttpContextDataProvider _httpContextDataProvider;
        private readonly IAttachmentService _attachmentConfigService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly MapperService _mapperService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsService"/> class.
        /// </summary>
        public AssetsService(ILogger<AssetsService> logService, IUnitOfWork unitOfWork, IHelperService helperService, IHttpContextDataProvider httpContextDataProvider, IAttachmentService attachmentConfigService, IHostEnvironment hostEnvironment)
        {
            _logService = logService;
            _unitOfWork = unitOfWork;
            _helpService = helperService;
            _httpContextDataProvider = httpContextDataProvider;
            _attachmentConfigService = attachmentConfigService;
            _hostEnvironment = hostEnvironment;
            _mapperService = _unitOfWork.MapperService;
        }

        #region SecurityGroup

        /// <summary>
        /// Retrieves a list of security groups.
        /// </summary>
        public async Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup(string lang)
        {
            try
            {
                List<SecurityGroup>? result = (await _unitOfWork.SecurityGroupRepository.GetAll()).ToList();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SecurityGroupMapper.MapCollectionToDataModel(result), result.Count);

            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<SecurityGroupDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Checks if a security group exists based on the provided form.
        /// </summary>
        public async Task<bool> SecurityGroupExists(SecurityGroupFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.SecurityGroupRepository.AnyAsync(x => x.Name == form.Name && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Adds a new security group.
        /// </summary>
        public async Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form, string lang)
        {
            using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (await SecurityGroupExists(form))
                {
                    return _helpService.CreateErrorResponse<SecurityGroupDataModel>(MessageKeys.record_exist, lang); // Specify the errors
                }

                SecurityGroup? newItem = _mapperService.SecurityGroupMapper.MapDataFormToEntity(form);

                await _unitOfWork.SecurityGroupRepository.InsertAsync(newItem, true);
                await transaction.CommitAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.SecurityGroupMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception exp)
            {
                await transaction.RollbackAsync();
                transaction.Dispose();
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<SecurityGroupDataModel>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Deletes a security group by ID.
        /// </summary>
        public async Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId, string lang)
        {
            using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                SecurityGroup? securityGroup = await _unitOfWork.SecurityGroupRepository.GetAsync(x => x.Id == securityGroupId);
                if (securityGroup == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                List<UserSecurityGroup> userSecurityGroups = await _unitOfWork.Context.UserSecurityGroups.Where(x => x.SecurityGroupId == securityGroup.Id).ToListAsync();

                foreach (UserSecurityGroup? item in userSecurityGroups)
                {
                    item.IsDeleted = true;
                }

                _unitOfWork.Context.UserSecurityGroups.UpdateRange(userSecurityGroups);

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                await transaction.RollbackAsync();
                transaction.Dispose();
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Retrieves a list of feature permissions.
        /// </summary>
        public async Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission(string lang)
        {
            try
            {
                List<FeaturePermission>? result = await _unitOfWork.Context.FeaturePermissions.AsNoTracking().ToListAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.FeaturePermissionMapper.MapCollectionToDataModel(result), result.Count);

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
                    await _unitOfWork.Context.GroupFeaturePermissions.AddAsync(new GroupFeaturePermission() { FeaturePermissionId = featurePermissionId, SecurityGroupId = securityGroupId });
                    await _unitOfWork.CompleteAsync();

                    return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
                }
                else
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_exist, lang); // Specify the errors
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
                GroupFeaturePermission? groupFeaturePermission = await _unitOfWork.Context.GroupFeaturePermissions.FirstOrDefaultAsync(x => x.SecurityGroupId == securityGroupId && x.FeaturePermissionId == featurePermissionId);
                if (groupFeaturePermission != null)
                {
                    _unitOfWork.Context.GroupFeaturePermissions.Remove(groupFeaturePermission);
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

        #region User

        /// <summary>
        /// Checks if a user with the given email already exists.
        /// </summary>
        public async Task<bool> UserExists(UserFormModel form, bool checkDifferentId = true)
        {
            return await _unitOfWork.UserRepository.AnyAsync(x => x.Email == form.Email && x.IsDeleted == false && (!checkDifferentId || x.Id != form.Id));
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        public async Task<GenericResponse<UserDataModel>> AddUser(UserFormModel form, string lang)
        {
            using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (await UserExists(form))
                {
                    return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.record_exist, lang);
                }

                AttachmentDataModel? UserImage = null;
                if (form.Image != null)
                {
                    UserImage = await _attachmentConfigService.AddAttachment(form.Image, GlobalAttachmentLocationDefinitions.UserImage);
                    if (UserImage == null)
                    {
                        await transaction.RollbackAsync();
                        return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.file_upload_failed, lang);
                    }
                }

                User? newItem = _mapperService.UserMapper.MapDataFormToEntity(form);

                newItem.TwoFactorEnabled = true;
                newItem.Signature = "*";

                if (UserImage != null)
                {
                    newItem.ImageId = UserImage.Id;
                    newItem.Image = null;
                }

                await _unitOfWork.UserRepository.InsertAsync(newItem, true);
                await transaction.CommitAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.UserMapper.MapEntityToDataModel(newItem));
            }
            catch (Exception exp)
            {
                await transaction.RollbackAsync();
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        public async Task<GenericResponse<UserDataModel>> UpdateUser(UserFormModel form, string lang)
        {
            using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == form.Id!.Value);
                if (user == null)
                {
                    return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.record_notfound, lang);
                }

                if (await UserExists(form, true))
                {
                    return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.record_exist, lang);
                }

                AttachmentDataModel? UserImage = null;
                if (form.Image != null)
                {
                    UserImage = await _attachmentConfigService.AddAttachment(new AttachmentFormModel() { Extention = form.Image.Extention, FileContent = form.Image.FileContent, Name = form.Image.Name, Path = "" }, GlobalAttachmentLocationDefinitions.UserImage);
                    if (UserImage == null)
                    {
                        await transaction.RollbackAsync();
                        return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.file_upload_failed, lang);
                    }

                    if (user.ImageId != null)
                    {
                        await _attachmentConfigService.DeleteAttachment(user.ImageId.Value);
                    }

                    user.ImageId = UserImage.Id;
                    user.Image = null;
                }

                user = _mapperService.UserMapper.MapUpdateDataFormToEntity(form, user);
                user.TwoFactorEnabled = true;
                user.Signature = "*";

                await _unitOfWork.UserRepository.UpdateAsync(user, true);
                await transaction.CommitAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, _mapperService.UserMapper.MapEntityToDataModel(user));
            }
            catch (Exception exp)
            {
                await transaction.RollbackAsync();
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        public async Task<GenericResponse<bool>> DeleteUser(long userId, string lang)
        {
            using IDbContextTransaction? transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                if (user == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.record_notfound, lang);
                }

                await _unitOfWork.UserRepository.DeleteAsync(user);
                await transaction.CommitAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                await transaction.RollbackAsync();
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Unlocks a user's account.
        /// </summary>
        public async Task<GenericResponse<bool>> UnlockUser(long userId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);

                if (user == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                user.Lookout = false;
                user.LookoutEnd = DateTime.UtcNow.AddSeconds(-1);
                user.LookoutEnabled = true;
                user.AccessFaildCount = 0;
                await _unitOfWork.UserRepository.UpdateAsync(user, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Updates the activation status of a user.
        /// </summary>
        public async Task<GenericResponse<bool>> UpdateUserStatus(long userId, bool status, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);

                if (user == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                user.IsActive = status;
                await _unitOfWork.UserRepository.UpdateAsync(user, true);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Assigns a user to a specific module.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="moduleId">The ID of the module.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> AssignUserToModule(long userId, long moduleId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                Module? module = await _unitOfWork.ModuleRepository.GetAsync(x => x.Id == moduleId);

                if (user == null || module == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                await _unitOfWork.Context.UserModules.AddAsync(new UserModule() { ModuleId = module.Id, UserId = user.Id, IsDeleted = false });
                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Assigns a user to a specific role.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="roleId">The ID of the role.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> AssignUserToRole(long userId, long roleId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                Role? role = await _unitOfWork.Context.Roles.FindAsync(roleId);

                if (user == null || role == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                await _unitOfWork.Context.UserRoles.AddAsync(new UserRole() { RoleId = role.Id, UserId = user.Id, IsDeleted = false });
                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Assigns a user to a specific security group.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="securityGroupId">The ID of the security group.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> AssignUserToSecurityGroup(long userId, long securityGroupId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                SecurityGroup? securityGroup = await _unitOfWork.SecurityGroupRepository.GetAsync(x => x.Id == securityGroupId);

                if (user == null || securityGroup == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                await _unitOfWork.Context.UserSecurityGroups.AddAsync(new UserSecurityGroup() { SecurityGroupId = securityGroup.Id, UserId = user.Id, IsDeleted = false });
                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Assigns a user to a specific feature permission.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="FeaturePermissionId">The ID of the feature permission.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> AssignUserToFeaturePermission(long userId, long FeaturePermissionId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                FeaturePermission? featurePermission = await _unitOfWork.Context.FeaturePermissions.FindAsync(FeaturePermissionId);

                if (user == null || featurePermission == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                await _unitOfWork.Context.UserFeaturePermissions.AddAsync(new UserFeaturePermission() { FeaturePermissionId = featurePermission.Id, UserId = user.Id, IsDeleted = false });
                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Removes the assignment of a user from a specific module.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="moduleId">The ID of the module.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> UnAssignUserFromModule(long userId, long moduleId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                Module? module = await _unitOfWork.ModuleRepository.GetAsync(x => x.Id == moduleId);

                if (user == null || module == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                List<UserModule>? userModels = await _unitOfWork.Context.UserModules.Where(x => x.UserId == user.Id && x.ModuleId == module.Id && !x.IsDeleted).ToListAsync();
                if (userModels.Count > 0)
                {
                    _unitOfWork.Context.UserModules.RemoveRange(userModels);
                }

                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Removes the assignment of a user from a specific role.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="roleId">The ID of the role.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> UnAssignUserFromRole(long userId, long roleId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                Role? role = await _unitOfWork.Context.Roles.FindAsync(roleId);

                if (user == null || role == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                List<UserRole>? userRoles = await _unitOfWork.Context.UserRoles.Where(x => x.UserId == user.Id && x.RoleId == role.Id && !x.IsDeleted).ToListAsync();
                if (userRoles.Count > 0)
                {
                    _unitOfWork.Context.UserRoles.RemoveRange(userRoles);
                }

                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Removes the assignment of a user from a specific security group.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="securityGroupId">The ID of the security group.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> UnAssignUserFromSecurityGroup(long userId, long securityGroupId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                SecurityGroup? securityGroup = await _unitOfWork.SecurityGroupRepository.GetAsync(x => x.Id == securityGroupId);

                if (user == null || securityGroup == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                List<UserSecurityGroup>? userSecurityGroups = await _unitOfWork.Context.UserSecurityGroups.Where(x => x.UserId == user.Id && x.SecurityGroupId == securityGroup.Id && !x.IsDeleted).ToListAsync();
                if (userSecurityGroups.Count > 0)
                {
                    _unitOfWork.Context.UserSecurityGroups.RemoveRange(userSecurityGroups);
                }

                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Removes the assignment of a user from a specific feature permission.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="featurePermissionId">The ID of the feature permission.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response indicating success or failure.</returns>
        public async Task<GenericResponse<bool>> UnAssignUserFromFeaturePermission(long userId, long featurePermissionId, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == userId);
                FeaturePermission? featurePermission = await _unitOfWork.Context.FeaturePermissions.FindAsync(featurePermissionId);

                if (user == null || featurePermission == null)
                {
                    return _helpService.CreateErrorResponse<bool>(MessageKeys.invalid_parameter, lang);
                }

                List<UserFeaturePermission>? featurePermissions = await _unitOfWork.Context.UserFeaturePermissions.Where(x => x.UserId == user.Id && x.FeaturePermissionId == featurePermission.Id && !x.IsDeleted).ToListAsync();
                if (featurePermissions.Count > 0)
                {
                    _unitOfWork.Context.UserFeaturePermissions.RemoveRange(featurePermissions);
                }

                await _unitOfWork.Context.SaveChangesAsync();

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, true);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<bool>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Retrieves a list of users based on the provided filter criteria.
        /// </summary>
        /// <param name="filterModel">Filter criteria for the user list.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response containing the list of users or an error.</returns>
        public async Task<GenericResponse<List<UserDataModel>>> ListUsers(UserFilterModel filterModel, string lang)
        {
            try
            {
                IQueryable<User>? users = await _unitOfWork.UserRepository.FilterByAsync(filterModel);
                List<UserDataModel>? result = _mapperService.UserMapper.MapCollectionToDataModel(await users.ToListAsync());

                if (users.Any())
                {
                    foreach (User? user in users)
                    {
                        UserDataModel? userModel = result.FirstOrDefault(x => x.Id == user.Id);
                    }
                }

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, result, result.Count, filterModel.CurrentPage, filterModel.PageSize);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<List<UserDataModel>>(MessageKeys.system_error, lang, exp);
            }
        }

        /// <summary>
        /// Retrieves details of a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="lang">Language for error/success messages.</param>
        /// <returns>A generic response containing user details or an error.</returns>
        public async Task<GenericResponse<UserDataModel>> GetUser(long id, string lang)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.IGetAsync(x => x.Id == id).FirstOrDefaultAsync();

                if (user == null)
                    return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.record_notfound, lang);

                UserDataModel? result = _mapperService.UserMapper.MapEntityToDataModel(user);

                return _helpService.CreateSuccessResponse(MessageKeys.operation_success, lang, result);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return _helpService.CreateErrorResponse<UserDataModel>(MessageKeys.system_error, lang, exp);
            }
        }
        #endregion
    }
}
