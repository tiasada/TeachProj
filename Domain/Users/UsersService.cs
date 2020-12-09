using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Users
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository = new UsersRepository();
        
        public CreatedUserDTO Create(string name, string cpf, Profile profile, string password)
        {
            var user = new User(name, cpf, profile, password);
            var userVal = user.Validate();

            if (!userVal.isValid)
            {   
                return new CreatedUserDTO(userVal.errors);
            }
            
            _usersRepository.Add(user);
            return new CreatedUserDTO(user.Id);
        }

        public User GetByID(Guid id)
        {
            return _usersRepository.Get(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _usersRepository.GetAll();
        }
    }
}