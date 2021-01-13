using System;
using Domain.Common;
using Domain.Students;

namespace Domain.Classrooms
{
    public interface IClassroomsService : IService<Classroom>
    {
        CreatedEntityDTO Create(string name);
        
        string AddStudent(Guid id, Guid classId);

        string AddTeacher(Guid id, Guid classId);
        
        string AddSubjects(Guid id, string subjects);
        
        Student GetStudent(Guid classId, Guid studentId);
        Teacher GetTeacher(Guid classId, Guid teacherId);
    }
}