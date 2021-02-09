using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.MailServices;
using Domain.MailServices.Templates;
using Domain.Users;

namespace Domain.Students
{
    public class StudentsService : Service<Student>, IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IUsersService _usersService;
        
        public StudentsService(IStudentsRepository studentsRepository, IUsersService usersService) : base(studentsRepository)
        {
            _studentsRepository = studentsRepository;
            _usersService = usersService;
        }

        public CreatedEntityDTO Create(string name, string cpf, string phoneNumber, DateTime birthDate, string email, string registration)
        {
            if (_studentsRepository.Get(x => x.CPF == cpf || x.Registration == registration) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Student already exists"});
            }
            
            if (_studentsRepository.Get(x => x.Email == email) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Email already in use"});
            }

            var student = new Student(name, cpf, phoneNumber, birthDate, email, registration);
            
            var studentVal = student.Validate();
            if (!studentVal.isValid)
            {
                return new CreatedEntityDTO(studentVal.errors);
            }
            
            var userCreated = _usersService.Create(Profile.Student, registration, cpf);
            if (!userCreated.IsValid)
            {
                return new CreatedEntityDTO(userCreated.Errors);
            }

            if (!String.IsNullOrWhiteSpace(email))
            {
                var mailservice = new MailService();
                mailservice.Send(TemplateType.StudentRegistration, student);
            }

            var user = _usersService.Get(userCreated.Id);
            student.LinkUser(user);
            _studentsRepository.Add(student);
            return new CreatedEntityDTO(student.Id);
        }

        public override bool Remove(Guid id)
        {
            var student = _studentsRepository.Get(x => x.Id == id);
            if (student == null) { return false; }

            var user = _usersService.Get(student.UserId);
            if (user != null)
            {
                _usersService.Remove(user.Id);
            }

            if (_studentsRepository.Get(x => x.Id == student.Id) != null)
            {
                _studentsRepository.Remove(student);
            }
            
            return true;
        }
    }
}