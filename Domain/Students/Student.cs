using System.Collections.Generic;
using Domain.Classrooms;
using Domain.Grades;
using Domain.Common;
using System;
using Domain.Users;
using Domain.Parents;

namespace Domain.Students
{
    public class Student : Person
    {
        public string Registration { get; private set; }
        
        public virtual User User { get; private set; }
        public Guid UserId { get; private set; }
        
        public virtual Parent Parent { get; private set; } = null;
        public Guid? ParentId { get; private set; } = null;
        
        public virtual IList<ClassroomStudent> Classrooms { get; set; } = new List<ClassroomStudent>();
        public virtual IList<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

        public Student(string name, string cpf, string phoneNumber, DateTime birthDate, string email, string regist) : base(name, cpf, phoneNumber, birthDate, email)
        {
            Registration = regist;
        }

        protected Student() : base("", "", "", DateTime.MinValue, "") {}

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
