using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace QLTimViecLamSinhVien.Services
{
    public class MockEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Just log the email for now (you can implement actual email sending logic here)
            Console.WriteLine($"Email sent to {email} with subject: {subject}");
            return Task.CompletedTask;
        }
    }
}
