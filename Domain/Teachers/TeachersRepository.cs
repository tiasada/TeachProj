using Domain.Common;

namespace Domain.Teachers
{
    public class TeachersRepository : Repository<Teacher>, ITeachersRepository
    {
        public TeachersRepository(IRepository<Teacher> repository) : base(repository)
        {}
    }
}