using System;
using Domain.Infra.Generics;

namespace Domain.Students
{
    public interface IStudentsRepository : IRepository<Student>
    {
        string AddClass(Guid id, Guid classId);
    }
}