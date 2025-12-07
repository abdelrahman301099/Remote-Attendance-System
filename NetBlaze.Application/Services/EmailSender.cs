using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities.Identity;
using System.Net;
using System.Net.Mail;

namespace NetBlaze.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendPasswordResetCodeAsync(string email, string code, CancellationToken cancellationToken = default)
        {
            try
            {
                var section = _configuration.GetSection("EmailSettings");
                var host = section["Host"];
                var portStr = section["Port"];
                var username = section["Username"];
                var password = section["Password"];
                var from = section["From"];
                var enableSslStr = section["EnableSsl"];

                if (string.IsNullOrWhiteSpace(host) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(from))
                {
                    _logger.LogError("Email configuration is incomplete");
                    throw new InvalidOperationException("Email settings are not properly configured");
                }

                var port = int.TryParse(portStr, out var p) ? p : 587;
                var enableSsl = bool.TryParse(enableSslStr, out var s) ? s : true;

                using var smtp = new SmtpClient(host, port)
                {
                    EnableSsl = enableSsl,
                    Credentials = new NetworkCredential(username, password)
                };

                using var message = new MailMessage(from, email)
                {
                    Subject = "Password Reset Code",
                    Body = GetEmailBody(code),
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message, cancellationToken);
                _logger.LogInformation("Password reset code sent to {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset email to {Email}", email);
                throw;
            }
        }

        private string GetEmailBody(string code)
        {
            return $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #333;'>Password Reset Request</h2>
                    <p>You have requested to reset your password. Use the code below to reset your password:</p>
                    <div style='background-color: #f4f4f4; padding: 15px; text-align: center; font-size: 24px; font-weight: bold; letter-spacing: 5px; margin: 20px 0;'>
                        {code}
                    </div>
                    <p style='color: #666;'>This code will expire in 15 minutes.</p>
                    <p style='color: #666;'>If you didn't request this, please ignore this email.</p>
                </div>
            </body>
            </html>
        ";
        }
    }

}
