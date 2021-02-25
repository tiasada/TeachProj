using System;

namespace WebAPI.Controllers.Students
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Registration { get; set; }
    }
}