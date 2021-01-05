using System;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public interface IGradesRepository : IRepository<Grade>
    {
        string SetGrade(Guid id, Guid studentId, double value);
    }
}