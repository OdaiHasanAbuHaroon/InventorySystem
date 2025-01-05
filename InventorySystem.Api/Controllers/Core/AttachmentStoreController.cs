using InventorySystem.Shared.Interfaces.Services.Core;
using InventorySystem.Api.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Api.Controllers.Core
{
    /// <summary>
    /// Controller for managing attachments in the application.
    /// Provides APIs to retrieve attachments and handles file-related operations.
    /// </summary>
    [Route("api/Application/[controller]")]
    [ApiController]
    [Authorize]
    public class AttachmentStoreController : ControllerBaseExt
    {
        private readonly ILogger<AttachmentStoreController> _logService;
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentStoreController"/> class.
        /// </summary>
        /// <param name="logger">Logger for tracking operations and errors.</param>
        /// <param name="attachment">Service to handle attachment-related business logic.</param>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment.</param>
        public AttachmentStoreController(ILogger<AttachmentStoreController> logger, IAttachmentService attachment, IWebHostEnvironment webHostEnvironment)
        {
            _logService = logger;
            _attachmentService = attachment;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Retrieves an attachment by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the attachment.</param>
        /// <returns>
        /// A file result containing the attachment's stream, type, and file name if found.
        /// Returns <see cref="NotFoundResult"/> if the attachment is not found.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await _attachmentService.GetAttachment(id);
                if (result == null)
                {
                    return NotFound();
                }
                return File(result.Stream, result.FileType, result.FileName);
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return NotFound();
            }
        }

        /// <summary>
        /// Recursively retrieves all file paths in a directory and its subdirectories, relative to the root directory.
        /// </summary>
        /// <param name="root">The root directory path to use as a base for relative paths.</param>
        /// <param name="directoryPath">The directory to search for files.</param>
        /// <param name="FileNames">The list to populate with relative file paths.</param>
        private void GetFilesRecursively(string root, string directoryPath, List<string> FileNames)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                var x = filePath.Replace(root, "");
                FileNames.Add(x.Replace("\\", "/"));
            }

            // Recursively read all subdirectories
            foreach (var subdirectory in Directory.GetDirectories(directoryPath))
            {
                GetFilesRecursively(root, subdirectory, FileNames);
            }
        }
    }
}
