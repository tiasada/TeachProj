using System;
using System.Collections.Generic;

namespace Domain.Classrooms
{
    public class CreatedClassroomDTO
    {
        public Guid Id { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        
        public CreatedClassroomDTO(Guid id)
        {
            Id = id;
            IsValid = true;
        }
        public CreatedClassroomDTO(List<string> errors)
        {
            Errors = errors;
        }
    }
}