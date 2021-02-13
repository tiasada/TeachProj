using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.MailServices;
using Domain.MailServices.Templates;
using Domain.Users;

namespace Domain.Teachers
{
    public class TeachersService : Service<Teacher>, ITeachersService
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IUsersService _usersService;

        public TeachersService(ITeachersRepository teachersRepository,
                                IUsersService usersService) : base(teachersRepository)
        {
            _teachersRepository = teachersRepository;
            _usersService = usersService;
        }
        
        public CreatedEntityDTO Create(string name, string cpf, string phoneNumber, DateTime birthDate, string email)
        {
            if (_teachersRepository.Get(x => x.CPF == cpf) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Teacher already exists"});
            }

            if (_teachersRepository.Get(x => x.Email == email) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Email already in use"});
            }
            
            var teacher = new Teacher(name, cpf, phoneNumber, birthDate, email);
            
            var teacherVal = teacher.Validate();
            if (!teacherVal.isValid)
            {
                return new CreatedEntityDTO(teacherVal.errors);
            }

            var userCreated = _usersService.Create(Profile.Teacher, cpf, birthDate.ToString("ddMMyyyy"));
            if (!userCreated.IsValid)
            {
                return new CreatedEntityDTO(userCreated.Errors);
            }

            if (!String.IsNullOrWhiteSpace(email))
            {
                var mailservice = new MailService();
                mailservice.Send(TemplateType.TeacherRegistration, teacher);
            }

            var user = _usersService.Get(userCreated.Id);
            teacher.LinkUser(user);
            _teachersRepository.Add(teacher);
            return new CreatedEntityDTO(teacher.Id);
        }

        public override bool Remove(Guid id)
        {
            var teacher = _teachersRepository.Get(x => x.Id == id);
            if (teacher == null) { return false; }

            var user = _usersService.Get(teacher.UserId);
            if (user != null)
            {
                _usersService.Remove(user.Id);
            }

            if (_teachersRepository.Get(x => x.Id == teacher.Id) != null)
            {
                _teachersRepository.Remove(teacher);
            }
            
            return true;
        }
    }
}