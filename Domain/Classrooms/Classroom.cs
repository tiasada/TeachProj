using System.Collections.Generic;
using Domain.Entities;
using Domain.Students;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public virtual IList<Student> Students { get; set; } = new List<Student>();

        public Classroom(string name)
        {
            Name = name;
        }
    }
}
