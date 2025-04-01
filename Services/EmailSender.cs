using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QLTimViecLamSinhVien.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _emailAddress = "your-email@example.com"; // Địa chỉ email người gửi
        private readonly string _emailPassword = "your-email-password"; // Mật khẩu email

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.example.com") // Cấu hình server SMTP
            {
                Port = 587,
                Credentials = new NetworkCredential(_emailAddress, _emailPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }
    }
}
