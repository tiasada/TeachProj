using Domain.Common;
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
            var crypt = new Crypt();
            var cryptPassword = crypt.CreateMD5(password);

            var user = _usersRepository.Get(x => x.Username == username);
            if (user == null) { return new AuthResponse(); }
            
            return user.Password == cryptPassword
                ? new AuthResponse(user)
                : new AuthResponse();
        }
    }
}