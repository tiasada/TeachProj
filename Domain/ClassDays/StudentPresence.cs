using System;
using Domain.MailServices;
using Domain.Students;

namespace Domain.ClassDays
{
    public class StudentPresence
    {
        public virtual ClassDay ClassDay { get; set; }
        public Guid ClassDayId { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        
        public bool Present { get; set; }
        public string Reason { get; set; }

        public StudentPresence(ClassDay classDay, Student student, bool present = false, string reason = null)
        {
            ClassDay = classDay;
            ClassDayId = classDay.Id;
            Student = student;
            StudentId = student.Id;
            Present = present;
            Reason = reason;
            if (student.ParentId != null)
            { SendAbsenceEmail(student); }
        }

        private void SendAbsenceEmail(Student student)
        {
            var mailService = new MailService();
            mailService.Send(MailServices.Templates.TemplateType.Absence, student.Parent);
        }

        protected StudentPresence(){}
    }
}