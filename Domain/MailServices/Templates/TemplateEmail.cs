using Domain.Common;
using Domain.Parents;
using Domain.Students;

namespace Domain.MailServices.Templates
{
    public class TemplateEmail
    {
        public string Sender { get; set; } = "teach.noreply@gmail.com";
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public TemplateEmail(TemplateType type, Person receiver)
        {
            Receiver = receiver.Email;

            switch(type)
            {
                case TemplateType.ParentRegistration:
                    Subject = "Cadastro Teach";
                    Body = $"<h1>Você foi cadastrado como responsável no sistema Teach</h1>\n<h3>TEST</h3>";
                    break;
                
                case TemplateType.StudentRegistration:
                    Subject = "Cadastro Teach";
                    Body = $"<h1>Você foi cadastrado como estudante no sistema Teach</h1>\n<h3>Sua matrícula é {(receiver as Student).Registration}</h3>";
                    break;
                
                case TemplateType.TeacherRegistration:
                    Subject = "Cadastro Teach";
                    Body = "<h1>Você foi cadastrado como professor no sistema Teach</h1>";
                    break;
                
                case TemplateType.Absence:
                    Subject = "Ausência de Estudante";
                    Body = $"<h1>{(receiver as Parent).Student.Name} não estava presente na aula</h1>";
                    break;
            }
        }
    }
}