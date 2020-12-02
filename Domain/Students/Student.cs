using System.Collections.Generic;
using Domain.People;

namespace Domain.Students
{
    public class Student : Person
    {
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
