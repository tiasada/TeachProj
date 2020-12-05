using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.People;

namespace Domain.Students
{
    public class Student : Person
    {
        // Classroom IDs
        public virtual List<Classroom> Classrooms { get; private set; }
        public List<Guid> ClassIds { get; private set; } = new List<Guid>();
        
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
