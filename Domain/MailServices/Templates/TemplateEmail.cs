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
                    Subject = "Bem-Vindo(a) ao Teach!";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/parentRegistrationMsg.txt");
                    break;
                
                case TemplateType.StudentRegistration:
                    Subject = "Bem-Vindo(a) ao Teach!";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/studentRegistrationMsg.txt");
                    break;
                
                case TemplateType.TeacherRegistration:
                    Subject = "Bem-Vindo(a) ao Teach!";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/teacherRegistrationMsg.txt");
                    break;
                
                case TemplateType.Absence:
                    Subject = "Ausência de Estudante";
                    Body = File.ReadAllText("../Domain/MailServices/Templates/absenceMsg.txt");
                    break;
            }
        }
    }
}