using System.Net.Mail;

namespace InventorySystem.Shared.Interfaces.Services.Configuration
{
    public interface ISmtpService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, List<Attachment>? attachments, bool isHtml = true);
    }
}
