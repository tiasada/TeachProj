using System;
using Domain.Infra.Generics;

namespace Domain.Teachers
{
    public interface ITeachersRepository : IRepository<Teacher>
    {
        string AddClass(Guid id, Guid classId);
    }
}