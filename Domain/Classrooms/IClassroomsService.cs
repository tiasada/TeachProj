using System;
using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Classrooms
{
    public interface IClassroomsService : IService<Classroom>
    {
        CreatedClassroomDTO Create(string name);
        
        string AddStudent(Guid id, Guid classId);

        string AddTeacher(Guid id, Guid classId);
    }
}