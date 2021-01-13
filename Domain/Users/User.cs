using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Users
{
    public class User : Entity
    {
        public Profile Profile { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }

        public User(Profile profile, string username, string password)
        {
            Profile = profile;
            Username = username;
            Password = password;
        }

        // Cannot bind paramater fix
        protected User() {}

        public (List<string> errors, bool isValid) Validate()
        {
            var errs = new List<string>(); 
            
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
