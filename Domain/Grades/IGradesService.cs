using System;
using Domain.Common;

namespace Domain.Grades
{
    public interface IGradesService : IService<Grade>
    {
        CreatedEntityDTO Create(string name, string description, string subject, DateTime date, Guid classroomId);
        string SetGrade(Guid id, Guid studentId, double value);
        string CloseGrade(Guid id);
    }
}