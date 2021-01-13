using System;
using Domain.Common;

namespace Domain.ClassDays
{
    public interface IClassDaysService : IService<ClassDay>
    {
        CreatedEntityDTO Create(DateTime date, Guid classroomId, string notes);
        string SetPresence(Guid id, Guid studentId, bool present, string reason);
    }
}