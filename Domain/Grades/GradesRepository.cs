using Domain.Common;

namespace Domain.Grades
{
    public class GradesRepository : Repository<Grade>, IGradesRepository
    {
        public GradesRepository(IRepository<Grade> repository) : base(repository)
        {}
    }
}