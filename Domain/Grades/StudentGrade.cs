using Domain.Students;

namespace Domain.Grades
{
    public class StudentGrade
    {
        public virtual Grade BaseGrade { get; set; }
        public virtual Student Student { get; set; }
        public double? Grade { get; set; }
    }
}