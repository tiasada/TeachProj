using System;
using Domain.Entities;
using Domain.Students;

namespace Domain.Grades
{
    public class StudentGrade : Entity
    {
        public Guid BaseGradeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public virtual Grade BaseGrade { get; set; }
        public virtual Student Student { get; set; }
        
        public double? Grade { get; set; }

        public StudentGrade(Guid baseGrade, Guid student, double? grade)
        {
            BaseGradeId = baseGrade;
            StudentId = student;
            Grade = grade;
        }

        public StudentGrade(Guid baseGrade, Guid student) : this(baseGrade, student, null)
        {
        }

        protected StudentGrade(){}
    }
}