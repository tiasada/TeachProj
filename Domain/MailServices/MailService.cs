using Domain.Common;
using Domain.MailServices.Templates;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Domain.MailServices
{
    public class MailService
    {
        public void Send(TemplateType templateType, Person receiver)
        {
            var template = new TemplateEmail(templateType);
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("teach.noreply@gmail.com"));
            email.To.Add(MailboxAddress.Parse(receiver.Email));
            email.Subject = template.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = template.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("teach.noreply", "teachproj123");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}