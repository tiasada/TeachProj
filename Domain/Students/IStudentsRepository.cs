using System;
using Domain.Infra;

namespace Domain.Students
{
    public interface IStudentsRepository : IRepository<Student>
    {
        string AddClass(Guid id, Guid classId);
    }
}