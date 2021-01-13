using Domain.Common;

namespace Domain.Students
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        private readonly IRepository<Student> _repository;

        public StudentsRepository(IRepository<Student> repository) : base(repository)
        {}
    }
}