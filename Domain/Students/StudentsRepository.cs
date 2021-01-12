using Domain.Infra.Generics;

namespace Domain.Students
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
    }
}