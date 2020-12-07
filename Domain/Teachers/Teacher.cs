using System.Collections.Generic;
using Domain.Classrooms;
using Domain.People;

namespace Domain.Teachers
{
    public class Teacher : Person
    {
        public virtual IList<Classroom> Classrooms { get; set; } = new List<Classroom>();
        
        public Teacher(string name, string cpf) : base(name, cpf)
        {}

        protected Teacher() : base("", "") {}

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
