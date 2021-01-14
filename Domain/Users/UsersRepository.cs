using Domain.Common;

namespace Domain.Users
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(IRepository<User> repository) : base(repository)
        {}
    }
}