using Domain.Common;

namespace Domain.Users
{
    public interface IUsersService : IService<User>
    {
        CreatedEntityDTO Create(Profile profile, string username, string password);
    }
}