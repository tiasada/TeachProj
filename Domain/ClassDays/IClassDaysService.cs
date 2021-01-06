using System;
using Domain.Infra.Generics;

namespace Domain.ClassDays
{
    public interface IClassDaysService : IService<ClassDay>
    {
        CreatedClassDayDTO Create(DateTime date, Guid classroomId, string notes);
        string SetPresence(Guid id, Guid studentId, bool present, string reason);
    }
}