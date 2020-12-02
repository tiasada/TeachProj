using System;
using System.Collections.Generic;

namespace Domain.Students
{
    public class CreatedStudentDTO
    {
        public Guid Id { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        
        public CreatedStudentDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }
        public CreatedStudentDTO(List<string> errors)
        {
            Errors = errors;
        }
    }
}