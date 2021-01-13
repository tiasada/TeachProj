using System;
using System.Linq;
using Domain.Common;

namespace Domain.Classrooms
{
    public class ClassroomsRepository : Repository<Classroom>, IClassroomsRepository
    {
        private readonly IRepository<Classroom> _repository;

        public ClassroomsRepository(IRepository<Classroom> repository) : base(repository)
        {}
    }
}