using System;
using Domain.Entities;
using Domain.Students;

namespace Domain.ClassDays
{
    public class StudentPresence : Entity
    {
        public virtual ClassDay ClassDay { get; set; }
        public Guid ClassDayId { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        
        public bool? Present { get; set; } = null;
        public string Reason { get; set; } = null;

        public StudentPresence(Guid classDay, Guid student, bool present)
        {
            ClassDayId = classDay;
            StudentId = student;
        }

        public StudentPresence(Guid classDay, Guid student) : this(classDay, student, null)
        {
        }

        public StudentPresence(Guid classDay, Guid student, string reason) : this(classDay, student, false)
        {
            Reason = reason;
        }

        protected StudentPresence(){}
    }
}