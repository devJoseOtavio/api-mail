using System.Net;
using System.Net.Mail;

namespace MailSenderApi.Infra.Services
{
    public class MailService : IMailService
    {

        private String smtpAddress => "smtp.gmail.com";

        private int portNumber = 587;

        private String emailFromAddress = "otaviojosetrabalho@gmail.com";

        private string password = "teste";

        public void SendMail(string[] emails, string subject, string body, bool isHtml = false)
        {
            using(MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(emailFromAddress);
                AddMailMessage(mailMessage, emails);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;
                using(SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.Send(mailMessage);
                }
            }
        }

        private void AddMailMessage(MailMessage mailMessage, String[] emails)
        {
            foreach(var email in emails)
            {
                mailMessage.To.Add(email);
            }
        }
    }
}
