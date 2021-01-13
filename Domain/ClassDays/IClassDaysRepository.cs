using Domain.Common;

namespace Domain.ClassDays
{
    public interface IClassDaysRepository : IRepository<ClassDay>
    {
        void SetPresence(StudentPresence studentPresence);
    }
}