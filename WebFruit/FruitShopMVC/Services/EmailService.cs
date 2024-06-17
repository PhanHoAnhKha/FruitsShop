using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace FruitShopMVC.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailSettings["From"], emailSettings["From"]));
            emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Sử dụng đúng tùy chọn bảo mật cho cổng 587
                    await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    // Log lỗi tại đây
                    _logger.LogError(ex, "Error sending email");
                    throw new Exception("Error sending email", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
