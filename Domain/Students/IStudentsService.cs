using System;
using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Students
{
    public interface IStudentsService : IService<Student>
    {
        CreatedStudentDTO Create(string name, string cpf, string registration);
        
        string AddClass(Guid id, Guid classId);
    }
}