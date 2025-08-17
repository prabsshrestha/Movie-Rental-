using Prabesh.Dtos;
using Prabesh.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Prabesh.Repository
{
    public class ContactService 
    {
        private readonly SmtpSettings _smtpSettings;

        public ContactService()
        {
            _smtpSettings = new SmtpSettings
            {
                Host = ConfigurationManager.AppSettings["SmtpHost"],
                Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
                Email = ConfigurationManager.AppSettings["SmtpEmail"],
                Password = ConfigurationManager.AppSettings["SmtpPassword"]
            };
        }

        public async Task<EmailDto> SendMessageAsync(Contact model)
        {
            try
            {
                var from = new MailAddress(_smtpSettings.Email, "Movie Rental System");
                var to = new MailAddress(_smtpSettings.Email, "Admin");

                string subject = string.IsNullOrEmpty(model.Subject) ? "New Contact Message" : model.Subject;
                string body = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}";

                using (var message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.ReplyToList.Add(new MailAddress(model.Email));

                    using (var smtp = new SmtpClient
                    {
                        Host = _smtpSettings.Host,
                        Port = _smtpSettings.Port,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password)
                    })
                    {
                        await smtp.SendMailAsync(message);
                    }
                }

                return new EmailDto { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new EmailDto { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }
    }
}
