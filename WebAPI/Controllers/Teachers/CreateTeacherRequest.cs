using System;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers.Teachers
{
    public class CreateTeacherRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile Picture { get; set; }
    }
}