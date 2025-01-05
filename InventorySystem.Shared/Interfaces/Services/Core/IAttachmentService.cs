using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.DTOs.Business;

namespace InventorySystem.Shared.Interfaces.Services.Core
{
    /// <summary>
    /// Provides services for managing attachments, including retrieving, adding, and deleting files,
    /// as well as handling MIME type mappings and content types.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Retrieves a dictionary of MIME types mapped to their corresponding file extensions.
        /// </summary>
        /// <returns>
        /// A dictionary where the keys are file extensions (e.g., ".pdf") and the values are MIME types (e.g., "application/pdf").
        /// </returns>
        Dictionary<string, string> GetMimeTypes();

        /// <summary>
        /// Retrieves the content type (MIME type) of a file based on its path or extension.
        /// </summary>
        /// <param name="path">The file path or file name to determine the content type.</param>
        /// <returns>A string representing the MIME type of the file.</returns>
        string GetContentType(string path);

        /// <summary>
        /// Retrieves an attachment by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the attachment to retrieve.</param>
        /// <returns>
        /// A <see cref="DocumentExport"/> object representing the attachment, or null if not found.
        /// </returns>
        Task<DocumentExport?> GetAttachment(long id);

        /// <summary>
        /// Adds a new attachment to the system and saves it to the specified folder path.
        /// </summary>
        /// <param name="document">The attachment details provided in the form of <see cref="AttachmentFormModel"/>.</param>
        /// <param name="folderPath">The folder path where the attachment will be saved.</param>
        /// <returns>
        /// An <see cref="AttachmentDataModel"/> object representing the added attachment, or null if the operation fails.
        /// </returns>
        Task<AttachmentDataModel?> AddAttachment(AttachmentFormModel document, string folderPath);

        /// <summary>
        /// Deletes an attachment by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the attachment to delete.</param>
        /// <param name="softDelete">
        /// A boolean value indicating whether to perform a soft delete (default is false).
        /// If true, the attachment is marked as deleted without being permanently removed.
        /// </param>
        /// <returns>A boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAttachment(long id, bool softDelete = false);
    }
}
