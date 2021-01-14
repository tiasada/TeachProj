using System;
using Domain.Students;

namespace Domain.Grades
{
    public class StudentGrade
    {
        public Guid BaseGradeId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public virtual Grade BaseGrade { get; set; }
        public virtual Student Student { get; set; }
        
        public double Grade { get; set; }

        public StudentGrade(Grade baseGrade, Student student, double grade)
        {
            BaseGrade = baseGrade;
            BaseGradeId = baseGrade.Id;
            Student = student;
            StudentId = student.Id;
            Grade = grade;
        }

        protected StudentGrade(){}
    }
}