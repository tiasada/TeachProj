using Domain.Common;

namespace Domain.Grades
{
    public class GradesRepository : Repository<Grade>, IGradesRepository
    {
        private readonly IRepository<Grade> _repository;
        public GradesRepository(IRepository<Grade> repository) : base(repository)
        {}
    }
}