using System;
using Domain.Students;

namespace Domain.Classrooms
{
    public class ClassroomStudent
    {
        public Guid ClassroomId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public virtual Classroom Classroom { get; set; }
        public virtual Student Student { get; set; }
        
        public ClassroomStudent(Guid classroom, Guid student)
        {
            ClassroomId = classroom;
            StudentId = student;
        }

        protected ClassroomStudent(){}
    }
}