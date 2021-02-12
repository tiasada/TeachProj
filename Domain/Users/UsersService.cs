using Domain.Common;
using System.Collections.Generic;

namespace Domain.Users
{
    public class UsersService : Service<User>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ICrypt _crypt;
        public UsersService(IUsersRepository usersRepository, ICrypt crypt) : base(usersRepository)
        {
            _usersRepository = usersRepository;
            _crypt = crypt;
        }

        public CreatedEntityDTO Create(Profile profile, string username, string password)
        {
            if (_usersRepository.Get(x => x.Username == username) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Username already in use"});
            }
            
            var cryptPassword = _crypt.CreateMD5(password);

            var user = new User(profile, username, cryptPassword);
            var userVal = user.Validate();

            if (!userVal.isValid)
            {   
                return new CreatedEntityDTO(userVal.errors);
            }
            
            _usersRepository.Add(user);
            return new CreatedEntityDTO(user.Id);
        }
    }
}