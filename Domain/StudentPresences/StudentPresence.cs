using System;
using Domain.Classrooms;
using Domain.MailServices;
using Domain.Students;

namespace Domain.StudentPresences
{
    public class StudentPresence
    {
        public virtual Classroom Classroom { get; set; }
        public Guid ClassroomId { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        
        public bool Present { get; set; }
        public string Reason { get; set; }

        public StudentPresence(Classroom classroom, Student student, bool present = false, string reason = null)
        {
            Classroom = classroom;
            ClassroomId = classroom.Id;
            Student = student;
            StudentId = student.Id;
            Present = present;
            Reason = reason;
        }

        private void SendAbsenceEmail(Student student)
        {
            var mailService = new MailService();
            mailService.Send(MailServices.Templates.TemplateType.Absence, student.Parent);
        }

        protected StudentPresence(){}
    }
}