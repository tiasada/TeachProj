using System.IO;

namespace Domain.MailServices.Templates
{
    public class TemplateEmail
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public TemplateEmail(TemplateType type)
        {
            switch(type)
            {
                case TemplateType.ParentRegistration:
                    Subject = "Cadastro Teach";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/registrationMsg.txt");
                    break;
                
                case TemplateType.StudentRegistration:
                    Subject = "Cadastro Teach";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/registrationMsg.txt");
                    break;
                
                case TemplateType.TeacherRegistration:
                    Subject = "Cadastro Teach";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/registrationMsg.txt");
                    break;
                
                case TemplateType.Absence:
                    Subject = "Ausência de Estudante";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/teacherRegistrationMsg.txt");
                    break;
            }
        }
    }
}