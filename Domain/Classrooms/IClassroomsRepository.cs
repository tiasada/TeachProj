using System;
using Domain.Infra.Generics;

namespace Domain.Classrooms
{
    public interface IClassroomsRepository : IRepository<Classroom>
    {
        string AddStudent(Guid id, Guid classId);

        string AddTeacher(Guid id, Guid classId);

        string AddSubjects(Guid id, string subjects);
    }
}