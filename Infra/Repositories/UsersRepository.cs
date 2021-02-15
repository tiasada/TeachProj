using Domain.Users;

namespace Infra.Repositories
{
    public class UsersRepository : DatabaseRepository<User>, IUsersRepository
    {
    }
}