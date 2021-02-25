using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Common;

namespace Domain.Grades
{
    public class Grade : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Subject { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsClosed { get; set; }
        public DateTime? DateClosed { get; set; } = null;
        public virtual Classroom Classroom { get; private set; }
        public Guid ClassroomId { get; private set; }
        public virtual IList<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

        public Grade(string name, string description, string subject, DateTime date, Classroom classroom)
        {
            Name = name;
            Description = description;
            Subject = subject;
            Classroom = classroom;
            ClassroomId = classroom.Id;
            Date = date.Date;
        }

        protected Grade(){}
    }
}