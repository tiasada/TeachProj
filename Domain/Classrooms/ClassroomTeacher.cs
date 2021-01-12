using System;
using Domain.Teachers;

namespace Domain.Classrooms
{
    public class ClassroomTeacher
    {
        public Guid ClassroomId { get; set; }
        
        public Guid TeacherId { get; set; }
        
        public virtual Classroom Classroom { get; set; }
        public virtual Teacher Teacher { get; set; }
        
        public ClassroomTeacher(Guid classroom, Guid teacher)
        {
            ClassroomId = classroom;
            TeacherId = teacher;
        }

        protected ClassroomTeacher(){}
    }
}