using System;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public interface IGradesService : IService<Grade>
    {
        CreatedGradeDTO Create(string name, string description, string subject, DateTime date, Guid classroomId);
        string SetGrade(Guid id, Guid studentId, double value);
        string CloseGrade(Guid id);
    }
}