using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Users
{
    public class UsersService : Service<User>, IUsersService
    {
        private readonly new IUsersRepository _repository;
        public UsersService(UsersRepository usersRepository) : base(usersRepository)
        {}

        public CreatedUserDTO Create(Profile profile, string username, string password)
        {
            var user = new User(profile, username, password);
            var userVal = user.Validate();

            if (!userVal.isValid)
            {   
                return new CreatedUserDTO(userVal.errors);
            }
            
            _repository.Add(user);
            return new CreatedUserDTO(user.Id);
        }
    }
}