using System;
using System.Collections.Generic;

namespace Domain.Teachers
{
    public class CreatedTeacherDTO
    {
        public Guid Id { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        
        public CreatedTeacherDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }
        public CreatedTeacherDTO(List<string> errors)
        {
            Errors = errors;
        }
    }
}