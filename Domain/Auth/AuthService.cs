using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Users;

namespace Domain.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;

        public AuthService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public AuthResponse Login(string username, string password)
        {
            var user = _usersRepository.Get(x => x.Username == username);
            if (user == null) { return new AuthResponse(); }
            
            return user.Password == password
                ? new AuthResponse(user.Id)
                : new AuthResponse();
        }
    }
}