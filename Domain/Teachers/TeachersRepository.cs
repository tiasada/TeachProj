using Domain.Infra.Generics;

namespace Domain.Teachers
{
    public class TeachersRepository : Repository<Teacher>, ITeachersRepository
    {
        private readonly IRepository<Teacher> _repository;
        public TeachersRepository(IRepository<Teacher> repository)
        {
            _repository = repository;
        }
    }
}