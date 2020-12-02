using Domain.Users;

namespace WebAPI.Controllers.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public Profile Profile { get; set; }
        public string Password { get; set; }
    }
}