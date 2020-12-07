using System.Collections.Generic;
using Domain.Students;
using Domain.Classrooms;
using System;

namespace Domain.StudentClassrooms
{
    public class StudentClassroom
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
