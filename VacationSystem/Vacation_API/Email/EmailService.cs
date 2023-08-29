﻿using System.Net;
using System.Net.Mail;

namespace Vacation_API.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;   
        }
        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host")!;
                string name = _configuration.GetValue<string>("SMTP:Nome")!;
                string userName = _configuration.GetValue<string>("SMTP:userName")!;
                string password = _configuration.GetValue<string>("SMTP:Senha")!;
                int port = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, name),
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using(SmtpClient smtp = new SmtpClient(host,port))
                {
                    smtp.Credentials = new NetworkCredential(userName, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
