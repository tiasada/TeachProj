using System;
using System.Collections.Generic;
using Domain.Infra.Generics;

namespace Domain.Grades
{
    public class CreatedGradeDTO : CreatedEntityDTO
    {
        public CreatedGradeDTO(Guid id) : base(id)
        {}
        public CreatedGradeDTO(List<string> errors) : base(errors)
        {}
        public CreatedGradeDTO(string error) : base(error)
        {}
    }
}