using Domain.Common;

namespace Domain.ClassDays
{
    public class ClassDaysRepository : Repository<ClassDay>, IClassDaysRepository
    {
        private readonly IRelationalRepository<StudentPresence> _presenceRepository;
        public ClassDaysRepository(IRepository<ClassDay> repository, IRelationalRepository<StudentPresence> presenceRepository) : base(repository)
        {
            _presenceRepository = presenceRepository;
        }

        public void SetPresence(StudentPresence studentPresence)
        {
            _presenceRepository.Add(studentPresence);
        }
    }
}