using System.Collections.Generic;
using Domain.Grades;
using Domain.ClassDays;
using Domain.Common;
using System.Linq;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        private IList<string> _subjects { get; set; } = new List<string>();
        public IList<string> Subjects {
            get { return _subjects; }
            set { _subjects = value; }
        }
        public string SubjectsString {
            get { return string.Join(", ", _subjects); }
            set { _subjects = value.Split(", ", System.StringSplitOptions.RemoveEmptyEntries).ToList(); }
        }

        public virtual IList<ClassroomStudent> Students { get; set; } = new List<ClassroomStudent>();
        public virtual IList<ClassroomTeacher> Teachers { get; set; } = new List<ClassroomTeacher>();
        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
        public virtual IList<ClassDay> ClassDays { get; set; } = new List<ClassDay>();

        public Classroom(string name)
        {
            Name = name;
        }
    }
}
