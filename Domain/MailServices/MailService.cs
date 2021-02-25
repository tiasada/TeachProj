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
        //Debug tool, prevents spam during testing
        private bool serviceOn = true;
        
        public void Send(TemplateType templateType, Person receiver)
        {
            if (!serviceOn)
            { return; }
            
            var template = new TemplateEmail(templateType);
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("teach.noreply@gmail.com"));
            if (!MailboxAddress.TryParse(receiver.Email, out var receiverAddress))
            {
                return;
            }
            email.To.Add(receiverAddress);
            email.Subject = template.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = template.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("teach.noreply", "teachproj123");
            try
            {
                smtp.Send(email);
            }
            catch
            {
                
            }
            smtp.Disconnect(true);
        }
    }
}