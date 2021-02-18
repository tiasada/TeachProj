using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Grades;
using Domain.StudentPresences;
using Domain.Students;
using Domain.Teachers;

namespace Domain.Classrooms
{
    public interface IClassroomsService : IService<Classroom>
    {
        CreatedEntityDTO Create(string name);
        
        string AddStudent(Guid id, Guid classId);

        string AddTeacher(Guid id, Guid classId);
        
        string AddSubject(Guid id, string subject);

        string SetPresences(Guid classId, List<StudentPresence> presences);
        
        Student GetStudent(Guid classId, Guid studentId);
        IList<Student> GetStudents(Guid classId);
        Teacher GetTeacher(Guid classId, Guid teacherId);
        IList<Teacher> GetTeachers(Guid classId);
        IList<Grade> GetGrades(Guid classId);
        IList<Classroom> GetByTeacher(Guid id);
    }
}