using System;
using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Common;
using Domain.Users;

namespace Domain.Teachers
{
    public class Teacher : Person
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        
        public virtual IList<ClassroomTeacher> Classrooms { get; set; } = new List<ClassroomTeacher>();
        
        public Teacher(string name, string cpf, string phoneNumber, DateTime birthDate) : base(name, cpf, phoneNumber, birthDate)
        {
        }

        protected Teacher() : base("", "", "", DateTime.MinValue) {}

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
