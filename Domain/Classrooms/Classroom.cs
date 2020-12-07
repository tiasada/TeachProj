using System.Collections.Generic;
using Domain.Entities;
using Domain.StudentClassrooms;
using Domain.Students;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public IList<StudentClassroom> StudentClassrooms { get; set; }

        public Classroom(string name)
        {
            Name = name;
        }
    }
}
