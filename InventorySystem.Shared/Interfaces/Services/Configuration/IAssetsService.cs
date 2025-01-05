using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Configuration
{
    public interface IAssetsService
    {
        Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form, string lang);

        Task<GenericResponse<UserDataModel>> AddUser(UserFormModel form, string lang);

        Task<GenericResponse<bool>> AssignUserToFeaturePermission(long userId, long featurePermissionId, string lang);

        Task<GenericResponse<bool>> AssignUserToModule(long userId, long moduleId, string lang);

        Task<GenericResponse<bool>> AssignUserToRole(long userId, long roleId, string lang);

        Task<GenericResponse<bool>> AssignUserToSecurityGroup(long userId, long securityGroupId, string lang);

        Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId, string lang);

        Task<GenericResponse<bool>> DeleteUser(long id, string lang);

        Task<GenericResponse<UserDataModel>> GetUser(long id, string lang);

        Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup(string lang);

        Task<GenericResponse<List<UserDataModel>>> ListUsers(UserFilterModel filterModel, string lang);

        Task<GenericResponse<bool>> UnAssignUserFromFeaturePermission(long userId, long featurePermissionId, string lang);

        Task<GenericResponse<bool>> UnAssignUserFromModule(long userId, long moduleId, string lang);

        Task<GenericResponse<bool>> UnAssignUserFromRole(long userId, long roleId, string lang);

        Task<GenericResponse<bool>> UnAssignUserFromSecurityGroup(long userId, long securityGroupId, string lang);

        Task<GenericResponse<bool>> UnlockUser(long userId, string lang);

        Task<GenericResponse<UserDataModel>> UpdateUser(UserFormModel form, string lang);

        Task<GenericResponse<bool>> UpdateUserStatus(long userId, bool status, string lang);

        Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission(string lang);

        Task<GenericResponse<bool>> AssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang);

        Task<GenericResponse<bool>> UnAssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang);
    }
}
