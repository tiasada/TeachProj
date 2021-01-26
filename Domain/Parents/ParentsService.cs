using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Students;
using Domain.Users;

namespace Domain.Parents
{
    public class ParentsService : Service<Parent>, IParentsService
    {
        private readonly IParentsRepository _parentsRepository;
        private readonly IStudentsService _studentsService;
        private readonly IUsersService _usersService;
        
        public ParentsService(IParentsRepository parentsRepository, IUsersService usersService, IStudentsService studentsService) : base(parentsRepository)
        {
            _parentsRepository = parentsRepository;
            _usersService = usersService;
            _studentsService = studentsService;
        }

        public CreatedEntityDTO Create(string name, string cpf, string phoneNumber, Guid studentId)
        {
            if (_parentsRepository.Get(x => x.CPF == cpf) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Parent already exists"});
            }

            var student = _studentsService.Get(studentId);
            if (student != null)
            {
                return new CreatedEntityDTO(new List<string>{"Student not found"});
            }
            
            var parent = new Parent(name, cpf, phoneNumber, student);
            
            var parentVal = parent.Validate();
            if (!parentVal.isValid)
            {
                return new CreatedEntityDTO(parentVal.errors);
            }
            
            var userCreated = _usersService.Create(Profile.Parent, cpf, cpf);
            if (!userCreated.IsValid)
            {
                return new CreatedEntityDTO(userCreated.Errors);
            }

            var user = _usersService.Get(userCreated.Id);
            parent.LinkUser(user);
            _parentsRepository.Add(parent);
            return new CreatedEntityDTO(parent.Id);
        }

        public override bool Remove(Guid id)
        {
            var parent = _parentsRepository.Get(id);
            if (parent == null) { return false; }

            var user = _usersService.Get(parent.UserId);
            if (user != null)
            {
                _usersService.Remove(user.Id);
            }

            if (_parentsRepository.Get(parent.Id) != null)
            {
                _parentsRepository.Remove(parent);
            }
            
            return true;
        }
    }
}