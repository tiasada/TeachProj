using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Users
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository = new UsersRepository();
        
        public CreatedUserDTO Create(Profile profile, string username, string password)
        {
            var user = new User(profile, username, password);
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