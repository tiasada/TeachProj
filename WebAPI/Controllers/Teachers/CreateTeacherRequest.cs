using System;

namespace WebAPI.Controllers.Teachers
{
    public class CreateTeacherRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}