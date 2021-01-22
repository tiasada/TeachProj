using System.Collections.Generic;
using Domain.Common;
using System;
using Domain.Users;

namespace Domain.Students
{
    public class Parent : Person
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }

        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        
        public Parent(string name, string cpf, Student student) : base(name, cpf)
        {
            Student = student;
        }

        protected Parent() : base("", "") {}

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

        public void LinkUser(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}
