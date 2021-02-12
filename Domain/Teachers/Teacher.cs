using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Classrooms;
using Domain.Common;
using Domain.Users;

namespace Domain.Teachers
{
    public class Teacher : Person
    {
        // public byte[] Picture { get; set; }

        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        
        public virtual IList<ClassroomTeacher> Classrooms { get; set; } = new List<ClassroomTeacher>();
        
        public Teacher(string name, string cpf, string phoneNumber, DateTime birthDate, string email) : base(name, cpf, phoneNumber, birthDate, email)
        {
        }

        protected Teacher() : base("", "", "", DateTime.MinValue, "") {}

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

        private bool ValidateEmail()
        {
            return Regex.IsMatch(
                Email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase
            );
        }

        public void LinkUser(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}
