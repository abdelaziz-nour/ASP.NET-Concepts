using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TestCoreApp.Models
{
    public class clsEmailConfirm : IEmailSender
    {
        private readonly ILogger<clsEmailConfirm> _logger;
        private readonly string _sendGridApiKey;

        public clsEmailConfirm(IConfiguration configuration, ILogger<clsEmailConfirm> logger)
        {
            _sendGridApiKey = configuration["SendGrid:ApiKey"];
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(_sendGridApiKey))
            {
                _logger.LogError("SendGrid API Key is not configured.");
                return;
            }

            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("api.sender@hotmail.com", "API Sender");
            var to = new EmailAddress(email, "User");
            var plainTextContent = "";
            var htmlContent = $"<html><body>{htmlMessage}</body></html>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            _logger.LogInformation("Sending email...");
            _logger.LogInformation($"From: {from.Email} ({from.Name})");
            _logger.LogInformation($"To: {to.Email} ({to.Name})");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"HtmlContent: {htmlContent}");

            var response = await client.SendEmailAsync(msg);

            _logger.LogInformation($"Response Status Code: {response.StatusCode}");
            _logger.LogInformation($"Response Headers: {response.Headers}");
            _logger.LogInformation($"Response Body: {await response.Body.ReadAsStringAsync()}");
        }
    }
}
