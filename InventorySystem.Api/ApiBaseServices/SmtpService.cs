using System.Net;
using System.Net.Mail;
using InventorySystem.Shared.Interfaces.Services.Configuration;
using InventorySystem.Shared.Models.Communications;

namespace InventorySystem.Api.ApiBaseServices
{
    /// <summary>
    /// Provides functionality for sending emails using SMTP.
    /// </summary>
    public class SmtpService : ISmtpService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmtpService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance for accessing SMTP settings.</param>
        /// <param name="logger">The logging service for capturing logs.</param>
        public SmtpService(IConfiguration configuration, ILogger<SmtpService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Sends an email using the configured SMTP server.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <param name="attachments">A list of attachments to include in the email (optional).</param>
        /// <param name="isHtml">Indicates whether the email body is HTML (default is true).</param>
        /// <returns>
        /// A boolean value indicating whether the email was successfully sent.
        /// </returns>
        public async Task<bool> SendEmailAsync(string to, string subject, string body, List<Attachment>? attachments, bool isHtml = true)
        {
            try
            {
                string? EmailChannel = _configuration.GetValue<string>("EmailChannel");
                if (EmailChannel == null)
                {
                    _logger.LogWarning("EmailChannel Not Defined"); return false;
                }
                else
                {
                    if (EmailChannel == "Smtp")
                    {
                        var smtpConfig = _configuration.GetSection("SmtpConfig").Get<SmtpConfig>();
                        if (smtpConfig != null)
                        {
                            using (var client = new SmtpClient(smtpConfig.Host, smtpConfig.Port))
                            {
                                client.Credentials = new NetworkCredential(smtpConfig.Username, smtpConfig.Password);
                                client.EnableSsl = smtpConfig.EnableSsl;

                                var mailMessage = new MailMessage
                                {
                                    From = new MailAddress(smtpConfig.FromAddress, smtpConfig.FromName),
                                    Subject = subject,
                                    Body = body,
                                    IsBodyHtml = isHtml
                                };
                                mailMessage.To.Add(to);

                                if (attachments != null && attachments.Count > 0)
                                {
                                    foreach (var attachment in attachments)
                                    {
                                        mailMessage.Attachments.Add(attachment);
                                    }
                                }

                                await client.SendMailAsync(mailMessage);

                                return true;
                            }
                        }
                        else
                        {
                            _logger.LogWarning("SmtpConfig Not define / SmtpService :SendEmailAsync");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "An error occurred: {ErrorMessage} \n StackTrace: {StackTrace}", exp.Message, exp.StackTrace);

                return false;
            }
        }
    }
}
