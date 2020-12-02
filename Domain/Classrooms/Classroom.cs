using System.Collections.Generic;
using Domain.Entities;
using Domain.Students;

namespace Domain.Classrooms
{
    public class Classroom : Entity
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }
    }
}
