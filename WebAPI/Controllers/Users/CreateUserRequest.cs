using Domain.Users;

namespace WebAPI.Controllers.Users
{
    public class CreateUserRequest
    {
        public Profile Profile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}