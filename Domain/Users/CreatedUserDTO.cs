using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public class CreatedUserDTO
    {
        public Guid Id { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        public CreatedUserDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }
        public CreatedUserDTO(List<string> errors)
        {
            Errors = errors;
        }
    }
}