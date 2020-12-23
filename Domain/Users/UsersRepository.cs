using Domain.Infra.Generics;

namespace Domain.Users
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
    }
}