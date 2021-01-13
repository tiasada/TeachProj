using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Grades;
using Domain.Common;

namespace Domain.Students
{
    public class Student : Person
    {
        public string Registration { get; set; }
        public virtual IList<ClassroomStudent> Classrooms { get; set; } = new List<ClassroomStudent>();
        public virtual IList<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

        public Student(string name, string cpf, string regist) : base(name, cpf)
        {
            Registration = regist;
        }

        protected Student() : base("", "") {}

        public (List<string> errors, bool isValid) Validate()
        {
            var errs = new List<string>(); 
            
            if (!ValidateName())
            {
                errs.Add("Invalid name");
            }
            if (!ValidateCPF())
            {
                errs.Add("Invalid CPF");
            }
            return (errs, errs.Count == 0);
        }
    }
}
