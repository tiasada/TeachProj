using System;
using System.Linq;
using Domain.Infra;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public class GradesRepository : Repository<Grade>, IGradesRepository
    {
        private readonly IRepository<Grade> _repository;
        public GradesRepository(IRepository<Grade> repository)
        {
            _repository = repository;
        }
    }
}