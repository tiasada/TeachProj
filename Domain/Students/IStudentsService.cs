using Domain.Common;

namespace Domain.Students
{
    public interface IStudentsService : IService<Student>
    {
        CreatedEntityDTO Create(string name, string cpf, string registration);
    }
}