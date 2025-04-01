using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace QLTimViecLamSinhVien.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly string _sendGridApiKey;

        public SendGridEmailSender(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_sendGridApiKey);  // Create SendGridClient instance
            var from = new EmailAddress("no-reply@yourdomain.com", "YourApp");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            await client.SendEmailAsync(msg);  // Send the email
        }
    }
}
