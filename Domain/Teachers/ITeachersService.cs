using System;
using System.Collections.Generic;
using Domain.Infra;

namespace Domain.Teachers
{
    public interface ITeachersService : IService<Teacher>
    {
        CreatedTeacherDTO Create(string name, string cpf);
        
        string AddClass(Guid id, Guid classId);
    }
}