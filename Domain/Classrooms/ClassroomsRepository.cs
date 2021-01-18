using Domain.Common;

namespace Domain.Classrooms
{
    public class ClassroomsRepository : Repository<Classroom>, IClassroomsRepository
    {
        public ClassroomsRepository(IRepository<Classroom> repository) : base(repository)
        {}
    }
}