using System;
using System.Collections.Generic;
using Domain.People;

namespace Domain.Users
{
    public class User : Person
    {
        public Profile Profile { get; protected set; }
        public string Password { get; protected set; }

        public User(string name, string cpf, Profile profile, string password) : base(name, cpf)
        {
            Profile = profile;
            Password = password;
        }

        // Cannot bind paramater fix
        protected User() : base("", "") {}

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
            if (!ValidateProfile())
            {
                errs.Add("Invalid profile");
            }
            return (errs, errs.Count == 0);
        }
        
        private bool ValidateProfile()
        {
            if (!Enum.IsDefined(typeof(Profile), Profile))
            {
                return false;
            }

            return true;
        }
    }
}
