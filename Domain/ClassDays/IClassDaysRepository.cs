using System;
using Domain.Infra.Generics;

namespace Domain.ClassDays
{
    public interface IClassDaysRepository : IRepository<ClassDay>
    {
        string SetPresence(Guid id, Guid studentId, bool present, string reason);
    }
}