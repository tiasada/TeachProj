using Domain.Common;

namespace Domain.Parents
{
    public class ParentsRepository : Repository<Parent>, IParentsRepository
    {
        public ParentsRepository(IRepository<Parent> repository) : base(repository)
        {}
    }
}