using System;

namespace WebAPI.Controllers.Parents
{
    public class CreateParentRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public Guid StudentId { get; set; }
    }
}