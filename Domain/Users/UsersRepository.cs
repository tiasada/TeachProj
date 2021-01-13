using Domain.Common;

namespace Domain.Users
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        private readonly IRepository<User> _repository;

        public UsersRepository(IRepository<User> repository) : base(repository)
        {}
    }
}