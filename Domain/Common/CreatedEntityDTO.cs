using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public class CreatedEntityDTO
    {
        public Guid Id { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        
        public CreatedEntityDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }
        public CreatedEntityDTO(List<string> errors)
        {
            Errors = errors;
        }
        public CreatedEntityDTO(string error)
        {
            Errors = new List<string>{error};
        }
    }
}