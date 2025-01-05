using InventorySystem.Data.DataContext;
using InventorySystem.Shared.Definitions;
using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Services.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace InventorySystem.ServiceImplementation.Services.Core
{
    /// <summary>
    /// Service to handle file attachments, including adding, retrieving, saving, and deleting files.
    /// </summary>
    public class AttachmentService : IAttachmentService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<AttachmentService> _logService;
        private readonly IConfiguration _configuration;
        protected int MaxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
        protected string RootDocPath = "C:\\Documents";
        protected bool EnableAttachmentBackup = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentService"/> class.
        /// </summary>
        public AttachmentService(DatabaseContext context, ILogger<AttachmentService> logService, IConfiguration configuration, IHostEnvironment webHost)
        {
            _context = context;
            _logService = logService;
            _configuration = configuration;
            MaxFileSizeInBytes = _configuration.GetValue("MaxFileSizeInBytes", 5242880);
            EnableAttachmentBackup = _configuration.GetValue("EnableAttachmentBackup", true);
            RootDocPath = _configuration.GetValue<string>("RootConfigDocumentPath") ?? Path.Combine(webHost.ContentRootPath, "wwwroot", "ConfigDocuments");
        }

        /// <summary>
        /// Returns a dictionary of MIME types based on file extensions.
        /// </summary>
        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" },
                { ".bmp", "image/bmp" },
                { ".tif", "image/tiff" },
                { ".tiff", "image/tiff" },
                { ".svg", "image/svg+xml" },
                { ".ico", "image/x-icon" },
                { ".webp", "image/webp" },
                { ".heic", "image/heic" },
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".mp4", "video/mp4" },
                { ".avi", "video/x-msvideo" },
                { ".mov", "video/quicktime" },
                { ".wmv", "video/x-ms-wmv" },
                { ".flv", "video/x-flv" },
                { ".mkv", "video/x-matroska" },
                { ".zip", "application/zip" },
                { ".rar", "application/x-rar-compressed" },
                { ".7z", "application/x-7z-compressed" },
                { ".tar", "application/x-tar" },
                { ".gz", "application/gzip" },
                { ".html", "text/html" },
                { ".css", "text/css" },
                { ".js", "application/javascript" },
                { ".json", "application/json" },
                { ".xml", "application/xml" },
                { ".eot", "application/vnd.ms-fontobject" },
                { ".ttf", "font/ttf" },
                { ".woff", "font/woff" },
                { ".woff2", "font/woff2" }
            };
        }

        /// <summary>
        /// Gets the content type of a file based on its extension.
        /// </summary>
        public string GetContentType(string path)
        {
            Dictionary<string, string> types = GetMimeTypes();
            string? ext = Path.GetExtension(path).ToLowerInvariant();

            return types[ext];
        }

        /// <summary>
        /// Retrieves an attachment by its ID and prepares it for export.
        /// </summary>
        public async Task<DocumentExport?> GetAttachment(long id)
        {
            try
            {
                Attachment? attachment = await _context.Attachments.FindAsync(id);

                if (attachment == null || !Path.Exists(attachment.Path))
                    return null;

                MemoryStream memory = new();
                using (FileStream stream = new(attachment.Path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;

                return new DocumentExport()
                {
                    FileName = Path.GetFileName(attachment.Path),
                    FileType = GetContentType(attachment.Path),
                    Stream = memory
                };
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Adds an attachment and stores its backup if enabled.
        /// </summary>
        public async Task<AttachmentDataModel?> AddAttachment(AttachmentFormModel document, string folderPath)
        {
            try
            {
                AttachmentDataModel? saveResult = SaveAttachment(document, folderPath);

                if (saveResult == null)
                    return null;

                Attachment attachment = new()
                {
                    Extention = saveResult.Extention!,
                    Path = saveResult.Path!,
                    Name = saveResult.Name!
                };

                await _context.Attachments.AddAsync(attachment);
                await _context.SaveChangesAsync();

                if (EnableAttachmentBackup)
                {
                    await _context.AttachmentBackups.AddAsync(new AttachmentBackup()
                    {
                        Base64Content = document.FileContent,
                        AttachmentId = attachment.Id,
                        Name = attachment.Name,
                        Extention = attachment.Extention
                    });
                    await _context.SaveChangesAsync();
                }

                return new AttachmentDataModel()
                {
                    Id = attachment.Id,
                    Extention = attachment.Extention,
                    Name = attachment.Name,
                    Path = $"/AttachmentStore?id={attachment.Id}"
                };
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Saves the attachment to the file system.
        /// </summary>
        public AttachmentDataModel? SaveAttachment(AttachmentFormModel document, string folderPath)
        {
            try
            {
                string ext = document.Extention;
                DirectoryInfo directory = new(Path.Combine(RootDocPath, folderPath));

                if (!directory.Exists)
                    directory.Create();

                if (DocumentTypeDefinitions.DocumentTypes.Any(x => x.Equals(document.Extention, StringComparison.OrdinalIgnoreCase)))
                {
                    string fileName = $"{DateTime.UtcNow.Ticks}.{ext}";
                    string fullPath = Path.Combine(directory.FullName, fileName);
                    byte[] fileContent = Convert.FromBase64String(document.FileContent);
                    File.WriteAllBytes(fullPath, fileContent);

                    return new AttachmentDataModel()
                    {
                        Id = 0,
                        Extention = document.Extention,
                        Name = fileName,
                        FileContent = document.FileContent,
                        Path = fullPath
                    };
                }
                else if (ImageTypeDefinitions.ImageTypes.Any(x => x.Equals(document.Extention, StringComparison.OrdinalIgnoreCase)))
                {
                    ext = "jpeg";
                    string fileName = $"{DateTime.UtcNow.Ticks}.{ext}";
                    string fullPath = Path.Combine(directory.FullName, fileName);
                    byte[] fileContent = Convert.FromBase64String(document.FileContent);
                    using (MemoryStream inputStream = new(fileContent))
                    using (Image image = Image.Load(inputStream))
                    {
                        JpegEncoder encoder = new() { Quality = 75 };

                        using FileStream outputStream = new(fullPath, FileMode.Create);
                        image.Save(outputStream, encoder);
                    }

                    return new AttachmentDataModel()
                    {
                        Id = 0,
                        Extention = ext,
                        Name = fileName,
                        FileContent = document.FileContent,
                        Path = fullPath
                    };
                }

                return null;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Deletes an attachment from the system.
        /// </summary>
        public async Task<bool> DeleteAttachment(long id, bool softDelete = false)
        {
            try
            {
                Attachment? attachment = await _context.Attachments.FindAsync(id);
                if (attachment != null)
                {
                    if (softDelete)
                    {
                        attachment.IsDeleted = true;
                        _context.Attachments.Update(attachment);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        string fullPath = attachment.Path;
                        _context.Attachments.Remove(attachment);
                        await _context.SaveChangesAsync();
                        AttachmentBackup? backup = await _context.AttachmentBackups.FirstOrDefaultAsync(x => x.AttachmentId == id);

                        if (backup != null)
                        {
                            _context.AttachmentBackups.Remove(backup);
                            await _context.SaveChangesAsync();
                        }

                        File.Delete(fullPath);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception exp)
            {
                _logService.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);
                return false;
            }
        }
    }
}
