using System;

namespace WebAPI.Controllers.Parents
{
    public class CreateParentRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Registration { get; set; }
    }
}