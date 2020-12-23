using Domain.Infra.Generics;
using System.Collections.Generic;

namespace Domain.Users
{
    public class UsersService : Service<User>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository) : base(usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public CreatedUserDTO Create(Profile profile, string username, string password)
        {
            if (_usersRepository.Get(x => x.Username == username) != null)
            {
                return new CreatedUserDTO(new List<string>{"Username already in use"});
            }
            
            var user = new User(profile, username, password);
            var userVal = user.Validate();

            if (!userVal.isValid)
            {   
                return new CreatedUserDTO(userVal.errors);
            }
            
            _usersRepository.Add(user);
            return new CreatedUserDTO(user.Id);
        }
    }
}