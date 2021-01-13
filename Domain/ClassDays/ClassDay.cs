using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Common;

namespace Domain.ClassDays
{
    public class ClassDay : Entity
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public virtual Classroom Classroom { get; set; }
        public Guid ClassroomId { get; set; }
        public virtual IList<StudentPresence> StudentPresences { get; set; } = new List<StudentPresence>();

        public ClassDay(DateTime date, Classroom classroom, string notes)
        {
            Date = date.Date;
            Classroom = classroom;
            ClassroomId = classroom.Id;
            Notes = notes;
        }

        public ClassDay(DateTime date, Classroom classroom) : this(date, classroom, null)
        {
        }

        protected ClassDay(){}
    }
}