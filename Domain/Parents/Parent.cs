using System.Collections.Generic;
using Domain.Common;
using System;
using Domain.Users;
using Domain.Students;

namespace Domain.Parents
{
    public class Parent : Person
    {
        public virtual User User { get; private set; }
        public Guid UserId { get; private set; }

        public virtual Student Student { get; private set; }
        public Guid StudentId { get; private set; }
        
        public Parent(string name, string cpf, string phoneNumber, DateTime birthDate, string email, Student student) : base(name, cpf, phoneNumber, birthDate, email)
        {
            Student = student;
            StudentId = student.Id;
        }

        protected Parent() : base("", "", "", DateTime.MinValue, "") {}

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
            if (Email != null)
            {
                if (!ValidateEmail())
                {
                    errs.Add("Invalid Email");
                }
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
