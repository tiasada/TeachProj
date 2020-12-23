using System;
using Domain.Infra.Generics;

namespace Domain.Students
{
    public interface IStudentsService : IService<Student>
    {
        CreatedStudentDTO Create(string name, string cpf, string registration);
    }
}