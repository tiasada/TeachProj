using System.Collections.Generic;
using Domain.People;
using Domain.StudentClassrooms;

namespace Domain.Students
{
    public class Student : Person
    {
        public IList<StudentClassroom> StudentClassrooms { get; set; }
        
        public Student(string name, string cpf) : base(name, cpf)
        {}

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
