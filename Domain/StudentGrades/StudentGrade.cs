using System;
using Domain.Entities;
using Domain.Grades;
using Domain.Students;

namespace Domain.StudentGrades
{
    public class StudentGrade : Entity
    {
        public virtual Grade BaseGrade { get; set; }
        public Guid BaseGradeId { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        public double? Grade { get; set; }

        public StudentGrade(Guid baseId, Guid studentId, double? grade)
        {
            BaseGradeId = baseId;
            StudentId = studentId;
            Grade = grade;
        }

        protected StudentGrade(){}
    }
}