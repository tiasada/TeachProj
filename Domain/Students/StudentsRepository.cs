using Domain.Common;

namespace Domain.Students
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        public StudentsRepository(IRepository<Student> repository) : base(repository)
        {}
    }
}