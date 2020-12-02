using Domain.Users;

namespace WebAPI.Controllers.Students
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}