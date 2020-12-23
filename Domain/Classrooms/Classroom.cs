using System.Collections.Generic;
using Domain.Entities;
using Domain.Grades;
using Domain.Students;
using Domain.Teachers;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public virtual IList<Student> Students { get; set; } = new List<Student>();
        public virtual IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        public IList<Grade> Grades { get; set; } = new List<Grade>();

        public Classroom(string name)
        {
            Name = name;
        }
    }
}
