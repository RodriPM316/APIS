using System.Net;
using System.Net.Mail;

namespace CatalogosEmpleados.Servicios
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string mensaje);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarCorreoAsync(string destinatario, string asunto, string mensaje)
        {
            var settings = _config.GetSection("EmailSettings");

            var mail = new MailMessage
            {
                From = new MailAddress(settings["SenderEmail"]!, settings["SenderName"]),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true
            };
            mail.To.Add(destinatario);

            using var smtp = new SmtpClient(settings["SmtpServer"], int.Parse(settings["Port"]!))
            {
                Credentials = new NetworkCredential(settings["SenderEmail"], settings["Password"]),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
