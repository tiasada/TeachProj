using System.Collections.Generic;
using Domain.Entities;
using Domain.Grades;
using Domain.Students;
using Domain.Teachers;
using Domain.ClassDays;
using System.Linq;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public string Subjects { get; set; }
        public virtual IList<Student> Students { get; set; } = new List<Student>();
        public virtual IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
        public virtual IList<ClassDay> ClassDays { get; set; } = new List<ClassDay>();

        public Classroom(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetSubjects()
        {
            return Subjects.Split(',').ToList();
        }
    }
}
