using Domain.Common;

namespace Domain.ClassDays
{
    public class ClassDaysRepository : Repository<ClassDay>, IClassDaysRepository
    {
        private readonly IRepository<StudentPresence> _presenceRepository;
        public ClassDaysRepository(IRepository<ClassDay> repository, IRepository<StudentPresence> presenceRepository) : base(repository)
        {
            _presenceRepository = presenceRepository;
        }

        public void SetPresence(StudentPresence studentPresence)
        {
            _presenceRepository.Add(studentPresence);
        }
    }
}