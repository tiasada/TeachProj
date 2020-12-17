using System;
using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Classrooms
{
    public interface IClassroomsRepository : IRepository<Classroom>
    {
        string AddStudent(Guid id, Guid classId);

        string AddTeacher(Guid id, Guid classId);
    }
}