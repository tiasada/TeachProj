using System.Collections.Generic;
using Domain.Grades;
using Domain.Common;
using Domain.StudentPresences;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; private set; }

        public virtual IList<ClassroomStudent> Students { get; set; } = new List<ClassroomStudent>();
        public virtual IList<ClassroomTeacher> Teachers { get; set; } = new List<ClassroomTeacher>();
        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
        public virtual IList<StudentPresence> StudentPresences { get; set; } = new List<StudentPresence>();

        public Classroom(string name)
        {
            Name = name;
        }
    }
}
