using Domain.Infra.Generics;

namespace Domain.Teachers
{
    public interface ITeachersService : IService<Teacher>
    {
        CreatedTeacherDTO Create(string name, string cpf);
    }
}