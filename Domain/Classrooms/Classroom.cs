using System.Collections.Generic;
using Domain.Infra.Generics;
using Domain.Grades;
using Domain.ClassDays;
using System.Linq;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public string Subjects { get; set; }
        public virtual IList<ClassroomStudent> Students { get; set; } = new List<ClassroomStudent>();
        public virtual IList<ClassroomTeacher> Teachers { get; set; } = new List<ClassroomTeacher>();
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
