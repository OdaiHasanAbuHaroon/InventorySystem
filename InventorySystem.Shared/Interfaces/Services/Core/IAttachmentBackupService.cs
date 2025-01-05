using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    /// <summary>
    /// Provides services for managing attachment backups, including operations to list, retrieve, create, update, and delete attachment backups.
    /// </summary>
    public interface IAttachmentBackupService
    {
        /// <summary>
        /// Retrieves a list of attachment backups based on the specified filter criteria.
        /// </summary>
        /// <param name="filterModel">The filter criteria for retrieving the attachment backups.</param>
        /// <param name="lang">The language code for localized responses.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> containing a list of <see cref="AttachmentBackupDataModel"/> objects.</returns>
        Task<GenericResponse<List<AttachmentBackupDataModel>>> AttachmentBackupList(AttachmentBackupFilterModel filterModel, string lang);

        /// <summary>
        /// Retrieves a specific attachment backup by its identifier.
        /// </summary>
        /// <param name="Id">The identifier of the attachment backup to retrieve.</param>
        /// <param name="lang">The language code for localized responses.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> containing the <see cref="AttachmentBackupDataModel"/> for the specified ID.</returns>
        Task<GenericResponse<AttachmentBackupDataModel>> GetAttachmentBackup(long Id, string lang);

        /// <summary>
        /// Deletes an attachment backup by its identifier.
        /// </summary>
        /// <param name="Id">The identifier of the attachment backup to delete.</param>
        /// <param name="lang">The language code for localized responses.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> indicating whether the deletion was successful.</returns>
        Task<GenericResponse<bool>> DeleteAttachmentBackup(long Id, string lang);

        /// <summary>
        /// Updates an existing attachment backup with the provided form data.
        /// </summary>
        /// <param name="form">The form data containing the updated information for the attachment backup.</param>
        /// <param name="lang">The language code for localized responses.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> containing the updated <see cref="AttachmentBackupDataModel"/>.</returns>
        Task<GenericResponse<AttachmentBackupDataModel>> UpdateAttachmentBackup(AttachmentBackupFormModel form, string lang);

        /// <summary>
        /// Creates a new attachment backup with the provided form data.
        /// </summary>
        /// <param name="form">The form data for the new attachment backup.</param>
        /// <param name="lang">The language code for localized responses.</param>
        /// <returns>A <see cref="GenericResponse{T}"/> containing the created <see cref="AttachmentBackupDataModel"/>.</returns>
        Task<GenericResponse<AttachmentBackupDataModel>> CreateAttachmentBackup(AttachmentBackupFormModel form, string lang);
    }
}
