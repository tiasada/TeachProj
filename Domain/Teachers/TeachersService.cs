using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Users;

namespace Domain.Teachers
{
    public class TeachersService : Service<Teacher>, ITeachersService
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IUsersService _usersService;


        public TeachersService(ITeachersRepository teachersRepository, IUsersService usersService) : base(teachersRepository)
        {
            _teachersRepository = teachersRepository;
            _usersService = usersService;
        }
        
        public CreatedEntityDTO Create(string name, string cpf)
        {
            if (_teachersRepository.Get(x => x.CPF == cpf) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Teacher already exists"});
            }
            
            var teacher = new Teacher(name, cpf);
            
            var teacherVal = teacher.Validate();
            if (!teacherVal.isValid)
            {
                return new CreatedEntityDTO(teacherVal.errors);
            }

            var userCreated = _usersService.Create(Profile.Teacher, cpf, cpf);
            if (!userCreated.IsValid)
            {
                return new CreatedEntityDTO(userCreated.Errors);
            }

            var user = _usersService.Get(userCreated.Id);
            teacher.LinkUser(user);
            _teachersRepository.Add(teacher);
            return new CreatedEntityDTO(teacher.Id);
        }

        public override bool Remove(Guid id)
        {
            var teacher = _teachersRepository.Get(id);
            if (teacher == null) { return false; }

            var user = _usersService.Get(teacher.UserId);
            if (user != null)
            {
                _usersService.Remove(user.Id);
            }

            if (_teachersRepository.Get(teacher.Id) != null)
            {
                _teachersRepository.Remove(teacher);
            }
            
            return true;
        }
    }
}