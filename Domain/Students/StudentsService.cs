using System;
using System.Collections.Generic;
using Domain.Common;
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

        public CreatedEntityDTO Create(string name, string cpf, string registration)
        {
            if (_studentsRepository.Get(x => x.CPF == cpf || x.Registration == registration) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Student already exists"});
            }
            
            var student = new Student(name, cpf, registration);
            
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

            var user = _usersService.Get(userCreated.Id);
            student.LinkUser(user);
            _studentsRepository.Add(student);
            return new CreatedEntityDTO(student.Id);
        }

        public override bool Remove(Guid id)
        {
            var student = _studentsRepository.Get(id);
            if (student == null) { return false; }

            var user = _usersService.Get(student.UserId);
            if (user != null)
            {
                _usersService.Remove(user.Id);
            }

            if (_studentsRepository.Get(student.Id) != null)
            {
                _studentsRepository.Remove(student);
            }
            
            return true;
        }
    }
}