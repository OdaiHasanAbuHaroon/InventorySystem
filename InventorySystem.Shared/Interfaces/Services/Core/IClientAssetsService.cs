using InventorySystem.Shared.DTOs.Identity;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    public interface IClientAssetsService
    {
        /// <summary>
        /// Adds a new security group based on the specified form data and language.
        /// </summary>
        /// <param name="form">The form model containing the security group data.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing the created <see cref="SecurityGroupDataModel"/> object.</returns>
        Task<GenericResponse<SecurityGroupDataModel>> AddSecurityGroup(SecurityGroupFormModel form, string lang);

        /// <summary>
        /// Deletes a specific security group by its ID and language.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group to delete.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteSecurityGroup(long securityGroupId, string lang);

        /// <summary>
        /// Retrieves a list of security groups based on the specified language.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="SecurityGroupDataModel"/> objects.</returns>
        Task<GenericResponse<List<SecurityGroupDataModel>>> ListSecurityGroup(string lang);

        /// <summary>
        /// Retrieves a list of feature permissions based on the specified language.
        /// </summary>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response containing a list of <see cref="FeaturePermissionDataModel"/> objects.</returns>
        Task<GenericResponse<List<FeaturePermissionDataModel>>> ListFeaturePermission(string lang);

        /// <summary>
        /// Assigns a feature permission to a specific security group.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group.</param>
        /// <param name="featurePermissionId">The unique identifier of the feature permission to assign.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the assignment was successful.</returns>
        Task<GenericResponse<bool>> AssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang);

        /// <summary>
        /// Unassigns a feature permission from a specific security group.
        /// </summary>
        /// <param name="securityGroupId">The unique identifier of the security group.</param>
        /// <param name="featurePermissionId">The unique identifier of the feature permission to unassign.</param>
        /// <param name="lang">The language code for localizing the response.</param>
        /// <returns>A generic response indicating whether the unassignment was successful.</returns>
        Task<GenericResponse<bool>> UnAssignFeaturePermissionToSecurityGroup(long securityGroupId, long featurePermissionId, string lang);
    }

}
