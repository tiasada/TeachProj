using Domain.Users;

namespace Domain.Auth
{
    public class AuthResponse
    {
        public User User { get; set; }
        public bool IsValid { get; set; } = true;

        public AuthResponse(User user)
        {
            User = user;
        }

        public AuthResponse()
        {
            IsValid = false;
        }
    }
}